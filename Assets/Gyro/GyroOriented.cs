using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GyroOriented : MonoBehaviour
{
    //Gyro
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rotation;

    //Cam
    private WebCamTexture cam;
    public RawImage background;
    public AspectRatioFitter fit;

    private bool arReady = false;

    private LocationInfo locationInfo;

    private IEnumerator Start()
    {
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.Log("This device does not have Gyroscope");
            yield break;
        }

        // Richiedi i permessi di localizzazione su Android
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                yield return new WaitForSeconds(1f); // Aspetta un po' prima di continuare
            }

            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
            {
                Debug.Log("Location permission not granted. AR functionality may be limited.");
                yield break;
            }
        }

        // Abilita il servizio di localizzazione
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled");
            yield break;
        }

        Input.location.Start();

        //Back Camera
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            if (!WebCamTexture.devices[i].isFrontFacing)
            {
                cam = new WebCamTexture(WebCamTexture.devices[i].name, Screen.width, Screen.height);
                break;
            }
        }

        // If we did not find a back camera, exit
        if (cam == null)
        {
            Debug.Log("This device does not have back Camera");
        }

        // Both services are supported, let's enable them
        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyro = Input.gyro;
        gyro.enabled = true;
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 0, 0);
        rotation = new Quaternion(0, 0, 1f, 0);

        cam.Play();
        background.texture = cam;

        arReady = true;
    }

    private void Update()
    {
        if (arReady)
        {
            locationInfo = Input.location.lastData;

            // Update Camera
            float ratio = (float)cam.width / (float)cam.height;
            fit.aspectRatio = ratio;

            float scaleY = cam.videoVerticallyMirrored ? -1.0f : 1.0f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -cam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);

            // Update Gyroscope
            Quaternion gyroRotation = gyro.attitude * rotation;

            // Adjust the rotation based on the compass heading
            float compassHeading = Input.compass.trueHeading;
            gyroRotation *= Quaternion.Euler(0, compassHeading, 0);

            transform.localRotation = gyroRotation;
        }
    }
}

