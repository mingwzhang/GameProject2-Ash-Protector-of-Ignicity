using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCreditsHotfix : MonoBehaviour
{
   void Start()
   {

   }
   
    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Application.LoadLevel("Credits");
        }
    } 
}
