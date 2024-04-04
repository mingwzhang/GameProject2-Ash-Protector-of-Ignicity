using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DarkAsh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsPlayer;
    private GameObject character;


    //Health UI
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private HealthBar healthBar;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] private Transform aimer;
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform spikes;
    [SerializeField] private bool isDefense;
    private float spikesCD;
    private float spikesAtkTime;

    private bool chasing;

    private float specialAtkCharge = 0;
    [SerializeField] private Slider chargeSlider;

    private bool castSpecialAtk1;
    private bool castSpecialAtk2;


    //Detection
    [SerializeField] private float attackRange;
    [SerializeField] private float sightRange;

    [SerializeField] private bool playerInAttackRange;
    [SerializeField] private bool playerInSightRange;

    //Defeat boss and opens door

    [SerializeField] private GameObject door;

    private float AtkRangeBoost;


    // Animation
    Animator animator;
    Animator DoorAnimator;

    bool isSideStep;

    // if Ash lands on enemy's head
    bool steppedOn;


    private void Awake()
    {
        character = GameObject.Find("Character");
        player = character.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        DoorAnimator = door.GetComponent<Animator>();

        AtkRangeBoost = 150;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        spikesCD = 1.0f;
        spikesAtkTime = spikesCD;
        chasing = true;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Look at player, but only rotate y axis
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPosition);

        if (playerInSightRange & chasing)  ChasePlayer();
        if (steppedOn) DealsDmg();
        if (playerInAttackRange) AttackPlayer();
        chargeSlider.value = specialAtkCharge;


        if (playerInSightRange & playerInAttackRange & isSideStep) {
            StartCoroutine(SideStep());
        }
        if (!playerInAttackRange) ResetAttack();

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            DoorAnimator.SetBool("OpenDoor", true);
            Destroy(gameObject);
        }else if(health == 150)
        {
            castSpecialAtk2 = true;
            CastSpecialAttack();
        }
        else if(health <= 150)
        {
            SpikeAttack();
        }
    }
 
    private void SpikeAttack()
    {
        GameObject spikesSpawn = Instantiate(spikes.gameObject) as GameObject;
        spikes.position = new Vector3(transform.position.x + 3, -10, transform.position.z - 1);
    }
    private void ChasePlayer()
    {
        chasing = true;
        animator.SetBool("isRunning", true);
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            RandomMoving();
            //Attack code here
            aimer.LookAt(player.position);
            animator.SetBool("isAttack", true);

            Rigidbody rb = Instantiate(projectile, aimer.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(aimer.forward * 50f, ForceMode.Impulse);
            rb.AddForce(aimer.up *0 , ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            //special attack charge up
            specialAtkCharge += 1;

            if (specialAtkCharge > 5 && specialAtkCharge < 200)
            {
                castSpecialAtk1 = true;
                CastSpecialAttack();
            }
            else if(specialAtkCharge >= 200)
            {
                StopSpecialAttack();
            }
        }
    }

    private void RandomMoving()
    {
        float randomLeft = Random.Range(-walkPointRange, -5);
        float randomRight = Random.Range(5, walkPointRange);
        float random = Random.Range(0, 2);

        if(random == 0)
            agent.velocity = agent.transform.right * randomLeft;
        if (random == 1)
            agent.velocity = agent.transform.right * randomRight;
    }
    private void DealsDmg()
    {
        // player takes dmg when he touched the head
        character.GetComponent<AshPC>().SetHealth(-.05f);

        if (!steppedOn) character.GetComponent<AshPC>().SetHealth(0f);
        if (character.GetComponent<AshPC>().health <= 0) steppedOn = false; 

    }
    IEnumerator SideStep()
    {
        float randomLeft = Random.Range(-walkPointRange,0);
        float randomRight = Random.Range(0, walkPointRange);

        agent.velocity = agent.transform.right * randomLeft;
        yield return new WaitForSeconds(2);
        agent.velocity = agent.transform.right * randomRight;
        isSideStep = false;
        yield return new WaitForSeconds(2);
        isSideStep = true;
    }

    private void CastSpecialAttack()
    {

        //boost attack range (reset after StopSpecialAttack())
        attackRange = AtkRangeBoost;

        //ai stop moving when casting special attack
        agent.SetDestination(transform.position);
        agent.speed = 0;
        isSideStep = false;
        if (castSpecialAtk1 == true)
        {
            timeBetweenAttacks = -5;
            animator.SetBool("SpecialAttack", true);
            animator.SetBool("isAttack", false);
            castSpecialAtk1 = false;
        }
        if (castSpecialAtk2 == true)
        {
            animator.SetTrigger("SpecialAtk2");
        }

    }
    private void StopSpecialAttack()
    {
        agent.speed = 20;
        attackRange = 50;
        specialAtkCharge = 0;
        timeBetweenAttacks = 1.5f;
        ChasePlayer();
        animator.SetBool("SpecialAttack", false);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("isAttack", false);
    }
    private void DestoryedEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            steppedOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            steppedOn = false;
        }
    }

}