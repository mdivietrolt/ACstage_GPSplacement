using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticNorthLocator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);
    }
}
