using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTorches : MonoBehaviour
{
    public AshPC player;
    public float health;
    private bool isPickedUp;
    public float timerSet;
    private float realTimer;
    public GameObject torch;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Character")
        {
            player.SetHealth(health);
            torch.SetActive(false);
            player.PlayPickupAudio();
            gameObject.GetComponent<SphereCollider>().enabled = false;
            isPickedUp = true;
            realTimer = timerSet;
        }
    }

    void Update()
    {
        if(isPickedUp)
        {
            realTimer -= Time.deltaTime;
            if(realTimer <= 0)
            {
                torch.SetActive(true);
                gameObject.GetComponent<SphereCollider>().enabled = true;
                isPickedUp = false;
                realTimer = timerSet;
                
            }
        }
    }

}
