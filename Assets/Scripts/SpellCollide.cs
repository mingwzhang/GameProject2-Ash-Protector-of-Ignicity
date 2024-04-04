using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollide : MonoBehaviour
{
    //private GameObject character;

    private void Start()
    {
        //character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            other.GetComponent<AshPC>().SetHealth(-0.2f);
        }
        else if (other.tag == "Untagged")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
