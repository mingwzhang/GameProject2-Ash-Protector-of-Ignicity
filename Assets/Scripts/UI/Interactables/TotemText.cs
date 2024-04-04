using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemText : MonoBehaviour
{
    public Camera camera;
   
    




    public void Start()
    {
        camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();

    }

    void Update()
    {
       
        if (camera == null)
        {
            this.transform.LookAt(camera.transform.position);
        }
        else
        {
            this.transform.LookAt(camera.transform.position);
        }

    }

}
