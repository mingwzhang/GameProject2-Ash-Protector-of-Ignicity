using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 150;
    public GameObject parentObject;
    public bool isDead;
    public HealthBar healthBar;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);
        ResetHealth();
        healthBar.gameObject.SetActive(false);
    }
    public void DamageHealth(float damage)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && parentObject.GetComponent<Tempest>().isFighting)
        { 
            health -= damage;
            healthBar.SetHealth(health);
        }
        
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

    public void TurnOnHealthBar()
    {
        healthBar.gameObject.SetActive(true);
    }
}
