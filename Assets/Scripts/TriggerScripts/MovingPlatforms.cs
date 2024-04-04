using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        

        if (other.tag == "Player")
        {
            other.transform.parent = gameObject.transform;
            

        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
            other.transform.parent = null;
            

        }
    }
}
