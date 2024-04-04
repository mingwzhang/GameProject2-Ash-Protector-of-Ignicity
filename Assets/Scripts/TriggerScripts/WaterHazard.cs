using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterHazard : MonoBehaviour
{
    private float damage = -.5f;
    

    public GameObject deathText;
    public AshPC playerPC;
    public AudioSource audioCue;

    private float timer;
    private float timerSet = .25f;

    private bool isTakingDamage;

    //bool isPlaying =  false;

    private void Start()
    {
        timer = timerSet;
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isTakingDamage = true;
            /*if(!isPlaying)
            {
                isPlaying = true;
                if(GetComponent<AudioSource>() != null && !GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();

                }
                audioCue.Play();

            }*/

            
                
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTakingDamage = false;
        }
    }


    private void Update()
    {
        if(isTakingDamage)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && playerPC.health > 0)
            {
                
                playerPC.SetHealth(damage);
                Debug.Log("Health Deducted. Health: " + playerPC.health);
                timer = timerSet;
            }
            else if (playerPC.health <= 0)
            {
                isTakingDamage = false;
            }

        }
        


    }

}
