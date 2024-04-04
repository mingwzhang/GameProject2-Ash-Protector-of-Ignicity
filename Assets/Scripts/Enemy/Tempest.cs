using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Tempest : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private GameObject character;
    public GameObject tempestMesh;
    public GameObject tornado;
    public GameObject npcManager1;
    public GameObject npcManager2;
    public Animator anim;
    public float velocity;
    public BossHealth health;
    public float timer = 3;
    private float deathTimer = 3;

    private float walkTimer = 5;
    private float walkingHeight = -0.025f;
    private float flyingHeight = 2.33f;

    public GameObject rainCloud;
    private float rainCloudHeight = 0.03f;
    
    public GameObject spawnPoint;

    private float attackTimerSet;
    private float attackTimer;

    public bool isFighting;
    private bool isAttacking;
    private bool isWalking = true;
    private bool isSwitchingStance;
    public GameObject poof;



    public Animator fadeOut;
    private float sceneTimer = 4;
    private bool isSwitchingLevels;
    private bool isFirstWaveSpawned;
    private bool isSecondWaveSpawned;


    public GameObject enemyChar;
    private float spawnTimer;
    private float spawnTimerSet = 1;
    private bool isCasting;









    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        character = GameObject.FindGameObjectWithTag("Player");
        attackTimerSet = 1;
        attackTimer = attackTimerSet;
        isFighting = false;
        walkTimer = Random.Range(5f, 20f);
        spawnTimer = spawnTimerSet;
        npcManager2.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        target = character.GetComponent<Transform>();



        if (!health.isDead)
        {
            
            velocity = ((agent.velocity.x + agent.velocity.z));
            /*walkTimer -= Time.deltaTime;
            if(walkTimer <= 0)
            {
                isWalking = !isWalking;
                isSwitchingStance = true;
            }
            
            if(isSwitchingStance)
            {

                if(isWalking)
                {
                    anim.SetBool("isGrounded", true);
                    agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y - flyingHeight, agent.transform.position.z);
                    isSwitchingStance = false;
                }
                else
                {
                    
                    anim.SetBool("isGrounded", false);
                    agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y + flyingHeight, agent.transform.position.z);
                    isSwitchingStance = false;
                }
            }*/
            

            if (isAttacking)
            {
                agent.isStopped = true;
                
            }
        
            else
            {
                agent.isStopped = false;
                if (velocity < 0)
                {
                    velocity = velocity * -1;
                }
                anim.SetFloat("Velocity", velocity);

                float dist = Vector3.Distance(agent.transform.position, target.position);
                float maxDist = 65.0f;
                float minDist = 20.0f;
                if (dist < maxDist && dist > minDist)
                {
                    health.TurnOnHealthBar();
                    isFighting = true;
                    target = character.GetComponent<Transform>();
                    agent.SetDestination(target.position);
                }
                else
                {
                    gameObject.transform.LookAt(new Vector3(target.position.x, gameObject.transform.position.y, target.position.z));
                }
            }


            if (isFighting)
            {
                if(health.health <= ((health.maxHealth / 3) * 2))
                {
                    if(!isFirstWaveSpawned)
                    {
                        if(!isCasting)
                        {
                            isCasting = true;
                            anim.SetTrigger("SpawnAttack");
                        }

                        spawnTimer -= Time.deltaTime;
                        if(spawnTimer <= 0)
                        {

                            isFirstWaveSpawned = true;
                        
                            GameObject spawnedEnemy = Instantiate(enemyChar, new Vector3(gameObject.transform.position.x + 10f,gameObject.transform.position.y + 2f,gameObject.transform.position.z), gameObject.transform.rotation);
                            spawnedEnemy.GetComponentInChildren<EnemyHealth>().maxHealth = 20;
                            spawnedEnemy.GetComponentInChildren<EnemyHealth>().ResetHealth();
                            GameObject spawnedEnemy2 = Instantiate(enemyChar, new Vector3(gameObject.transform.position.x - 10f, gameObject.transform.position.y + 2f, gameObject.transform.position.z), gameObject.transform.rotation);
                            spawnedEnemy2.GetComponentInChildren<EnemyHealth>().maxHealth = 20;
                            spawnedEnemy2.GetComponentInChildren<EnemyHealth>().ResetHealth();
                            spawnTimer = spawnTimerSet;
                            isCasting = false;
                        }



                    }
                    if(isFirstWaveSpawned && !isSecondWaveSpawned && health.health <= health.maxHealth / 3)
                    {
                        if (!isCasting)
                        {
                            isCasting = true;
                            anim.SetTrigger("SpawnAttack");
                        }
                        spawnTimer -= Time.deltaTime;
                        if (spawnTimer <= 0)
                        {

                            isSecondWaveSpawned = true;
                            GameObject spawnedEnemy3 = Instantiate(enemyChar, new Vector3(gameObject.transform.position.x + 10f, gameObject.transform.position.y + 2f, gameObject.transform.position.z), gameObject.transform.rotation);
                            spawnedEnemy3.GetComponentInChildren<EnemyHealth>().maxHealth = 20;
                            spawnedEnemy3.GetComponentInChildren<EnemyHealth>().ResetHealth();
                            GameObject spawnedEnemy4 = Instantiate(enemyChar, new Vector3(gameObject.transform.position.x - 10f, gameObject.transform.position.y + 2f, gameObject.transform.position.z), gameObject.transform.rotation);
                            spawnedEnemy4.GetComponentInChildren<EnemyHealth>().maxHealth = 20;
                            spawnedEnemy4.GetComponentInChildren<EnemyHealth>().ResetHealth();
                            spawnTimer = spawnTimerSet;
                            isCasting = false;
                        }
                    }
                }

                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {

                    attackTimerSet = Random.Range(1f, 5f);
                    anim.SetTrigger("Attack");
                    attackTimer = attackTimerSet;
                }
            }
        }
        else
        {
            tornado.SetActive(false);
            anim.SetBool("isDead", true);

            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                npcManager1.SetActive(false);
                npcManager2.SetActive(true);
                //fadeOut.SetBool("isLevelCompleted", true);
                //isSwitchingLevels = true;
                gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(.0035f, 0f, .0035f);
                if (gameObject.transform.localScale.x <= 0)
                {
                    GameObject poof1 = Instantiate(poof, gameObject.transform.position, gameObject.transform.rotation);
                     
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                       
                    Destroy(gameObject);

                }
            }
        }


    }

    public void SpawnRainCloud()
    {
        
        GameObject spawnedCloud = Instantiate(rainCloud, new Vector3(spawnPoint.transform.position.x, rainCloudHeight, spawnPoint.transform.position.z), new Quaternion(spawnPoint.transform.rotation.x, spawnPoint.transform.rotation.y, spawnPoint.transform.rotation.z, spawnPoint.transform.rotation.w));
        
    }

    public void ToggleAttacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }



}
