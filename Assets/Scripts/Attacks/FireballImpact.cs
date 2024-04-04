using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballImpact : MonoBehaviour
{

    public GameObject explosion;
    public float damage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().DamageHealth(damage);

            GameObject particle = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }
        else if(other.CompareTag("Boss"))
        {
            other.GetComponent<BossHealth>().DamageHealth(damage);

            GameObject particle2 = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }

        else if (other.CompareTag("Dark_Ash"))
        {
            other.GetComponent<DarkAsh>().TakeDamage(damage);

            GameObject particle3 = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player") && other.name != "Rain" && !other.CompareTag("Trigger"))
        {
            GameObject particle4 = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }




    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
