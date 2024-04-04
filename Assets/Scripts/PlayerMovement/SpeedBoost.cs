using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private float timer;
    public float timerSet;
    public float boostSpeed;
    private bool isBoosted;
    private Collider player;
    public SphereCollider collision;
    public GameObject bootMesh;


    // Start is called before the first frame update
    void Start()
    {
        collision = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            collision.enabled = false;
            player = other;
            player.GetComponent<AshPC>().ToggleSpeed(boostSpeed);
            isBoosted = true;
            bootMesh.SetActive(false);
            
            
        }
    }

    private void Update()
    {
        if(isBoosted)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                player.GetComponent<AshPC>().ToggleSpeed(boostSpeed);
                isBoosted = false;
                collision.enabled = true;
                bootMesh.SetActive(true);
            }
        }
        else
        {

            timer = timerSet;
        }
    }




}
