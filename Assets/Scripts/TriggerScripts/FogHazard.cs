using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FogHazard : MonoBehaviour
{
    private float timer;
    public float timerSet;
    public GameObject ppStandard;
    public GameObject ppFog;
    
    // Start is called before the first frame update
    void Start()
    {
        //ppFog = GameObject.Find("FogPostProcess");
        ppFog.SetActive(false);
        timer = timerSet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ppStandard.SetActive(false);
            ppFog.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            ppFog.SetActive(false);
            ppStandard.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                other.GetComponent<AshPC>().SetHealth(-0.25f);
                timer = timerSet;
            }
        }
    }
}
