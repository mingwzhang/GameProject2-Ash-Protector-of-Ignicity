using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SensativityController : MonoBehaviour
{
    private Slider slider;
    public CamController cam;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if(GameObject.Find("PlayerCamera").GetComponent<CamController>() != null)
        {
            cam = GameObject.Find("PlayerCamera").GetComponent<CamController>();

        }

        UpdateValueOnChange(slider.value);
        slider.onValueChanged.AddListener(delegate { UpdateValueOnChange(slider.value); });
    }

    public void UpdateValueOnChange(float value)
    {
        cam.SetRotSpeed(value);
    }

    

}
