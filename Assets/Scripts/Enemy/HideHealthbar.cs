using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHealthbar : MonoBehaviour
{
    private float dist;
    private float maxDist = 40;
    public GameObject healthBar;
    private EnemyHealth enemy;
    private GameObject player;
    private bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyHealth>();
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if(dist >= maxDist)
        {
            if(isOn)
            {
                healthBar.SetActive(false);
                isOn = false;
            }

        }
        else
        {
            if(!isOn)
            {
                healthBar.SetActive(true);
                enemy.ResetHealth();
                isOn = true;
                
            }

        }
    }
}
