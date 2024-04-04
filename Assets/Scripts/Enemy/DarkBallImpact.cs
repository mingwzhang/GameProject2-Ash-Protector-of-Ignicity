using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBallImpact : MonoBehaviour
{
 
    public GameObject explosion;

    private GameObject character;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject particle = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            character.GetComponent<AshPC>().SetHealth(-0.5f);
        }
        else if (other.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
            GameObject particle1 = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
