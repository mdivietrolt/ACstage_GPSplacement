using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCompass : MonoBehaviour
{
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = targetObject.transform.position;
        Vector3 relativeTarget = transform.parent.InverseTransformPoint(target);
        float needleRotation = Mathf.Atan2(relativeTarget.x, relativeTarget.z) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, needleRotation, 0);
        Debug.Log(needleRotation);
    }

    void OnGUI()
    {
        GUILayout.Label("Magnetometer reading: " + Input.compass.rawVector.ToString());
    }
}
