using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rain : MonoBehaviour
{
    public float damage;
    float damageTimer = .25f;

    
    public AshPC playerPC;
    

    public void Start()
    {
        playerPC = GameObject.Find("Character").GetComponent<AshPC>();
    }





    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            

            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                playerPC.SetHealth(damage);
                
                damageTimer = .25f;
            }
        }

    }


    private void Update()
    {

    }

}
