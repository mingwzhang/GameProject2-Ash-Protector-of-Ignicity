using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloudProjectileScript : MonoBehaviour
{
    private AshPC player;
    private float force = 30;
    private Vector3 dir;
    private float lifetime = 1.5f;
    public float damage = -.75f;
    public GameObject poof;
    public GameObject cloud;


    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = player.transform.position - gameObject.transform.position;
        dir = dir.normalized;
        dir = new Vector3(dir.x, 0, dir.z);

        gameObject.GetComponent<Rigidbody>().AddForce(dir * force);

        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(.025f, 0f, .025f);
            if(gameObject.transform.localScale.x <= 0)
            {
                GameObject poof1 = Instantiate(poof, cloud.transform.position, cloud.transform.rotation);
                Destroy(gameObject);

            }
        }

    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            
            player.SetHealth(damage);

             
            
        }

    }


  

}

    

