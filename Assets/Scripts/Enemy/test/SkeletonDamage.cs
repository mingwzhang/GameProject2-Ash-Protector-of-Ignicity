using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    private GameObject player;
    private bool isDamaging;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDamaging)
        {

            player.GetComponent<AshPC>().SetHealth(-.01f);
            if(player.GetComponent<AshPC>().health <= 0)
            {
                isDamaging = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDamaging = true;
            player = other.gameObject;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isDamaging = false;
            
        }
    }
}
