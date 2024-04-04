using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private GameObject character;
    public Animator anim;
    public float velocity;
    public EnemyHealth health;
    public float timer = 3;
    public Transform rainCloud;
    public GameObject poofCloud;
    public GameObject rainParticle;
    public GameObject cloud;
    private bool isCloudGone;


    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        character = GameObject.FindGameObjectWithTag("Player");
        health.maxHealth = 20;
        health.ResetHealth();

    }

    // Update is called once per frame
    void Update()
    {
        target = character.GetComponent<Transform>();
        if (!health.isDead)
        {
            float dist = Vector3.Distance(agent.transform.position, target.position);
            float minDist = 40.0f;
            if (dist < minDist)
            {
                
                target = character.GetComponent<Transform>();
                agent.SetDestination(target.position);
                velocity = agent.speed;
                anim.SetFloat("velocity", velocity);
            }

        }

        else
        {
            if (!isCloudGone)
            {
                rainParticle.SetActive(false);
                cloud.transform.localScale -= new Vector3(.1f, .1f, .1f);
                agent.isStopped = true;
                anim.SetBool("isDead", true);

                if(cloud.transform.localScale.x <= 0)
                {
                
                
                    GameObject poof = Instantiate(poofCloud, rainCloud.position, rainCloud.rotation);
                    Destroy(cloud);
                    isCloudGone = true;
                
                }
            }
            else
            {
                gameObject.transform.localScale -= new Vector3(.025f, .01f, .025f);
                    
                if (gameObject.transform.localScale.x <= 0f)
                {
                    GameObject poof2 = Instantiate(poofCloud, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(gameObject);
                }

            }
            
        }
        

    }

    



}
