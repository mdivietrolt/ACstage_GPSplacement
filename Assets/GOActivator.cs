using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOActivator : MonoBehaviour
{
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        if(go != null)
        {
            go.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
