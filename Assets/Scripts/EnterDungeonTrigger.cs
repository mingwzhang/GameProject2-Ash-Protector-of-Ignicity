using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDungeonTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          Application.LoadLevel("Temple Room");
        }
    } 
}
