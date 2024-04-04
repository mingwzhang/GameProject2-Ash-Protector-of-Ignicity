using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public Text objText;
    

    private void Start()
    {
        objText = GameObject.Find("ObjectiveText").GetComponent<Text>();
        objText.enabled = false;
        
    }
    public void UpdateObjText(string newText)
    {
        if(objText.IsActive() == false)
        {
            objText.enabled = true;
        }
        objText.text = newText;
    }

    
    
}
