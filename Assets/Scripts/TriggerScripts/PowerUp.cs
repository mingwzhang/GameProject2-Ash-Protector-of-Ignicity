using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AshPC playerPC;
    private bool pickedUp;
    private float timer;
    private float timerSet = 15;
    public GameObject stick;


    private void Start()
    {
        playerPC = GameObject.Find("Character").GetComponent<AshPC>();
        timer = timerSet;
    }


    // Start is called before the first frame update
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Pickup();
        }
    }

    private void Update()
    {
        if(pickedUp)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                stick.SetActive(true);
                gameObject.GetComponent<SphereCollider>().enabled = true;
                pickedUp = false;
                timer = timerSet;
            }
        }
    }
    // Update is called once per frame
    void Pickup()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Debug.Log("Picked up! Added 5 health.");
        //Destroy(gameObject);
        stick.SetActive(false);
        playerPC.IncreaseHealth();
        playerPC.SetHealth(5);
        playerPC.PlayPickupAudio();
        pickedUp = true;

    }
}
