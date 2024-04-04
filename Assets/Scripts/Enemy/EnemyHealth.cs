using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 20;
    public float maxHealth = 20;
    public GameObject parentObject;
    public bool isDead;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);
        ResetHealth();
    }
    public void DamageHealth(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isDead = true;
            //Destroy(parentObject);
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthBar.SetHealth(health);
    }
}
