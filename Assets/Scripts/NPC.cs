using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator anim;
    private bool waving;
    private float timer;
    public float timerSet;
    public SphereCollider sphereCollider;
    private float timeToDisplayText = 3.1f;
    

    private void Start()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        timer = timerSet;
        sphereCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("isWaving", true);
            waving = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        timeToDisplayText -= Time.deltaTime;
        if(timeToDisplayText <= 0)
        {
            sphereCollider.enabled = true;
        }
        if(waving)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                anim.SetBool("isWaving", false);
                waving = false;
                timer = timerSet;
            }
        }    
    }
}
