using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    GameObject vfx_1;
    // Start is called before the first frame update
    void Start()
    {
        vfx_1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
