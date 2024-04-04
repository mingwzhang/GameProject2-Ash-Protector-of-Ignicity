using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkeletonMageAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private Transform aimer;

    [SerializeField] private GameObject hpBar;
    [SerializeField] private LayerMask whatIsPlayer;
    private GameObject character;

    // Detection
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool hpAppearInRange;

    [SerializeField] private float sightRange;
    [SerializeField] private float hpAppearRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] private GameObject projectile;

    private float specialAtkCharge = 0;
    private bool castSpecialAtk;


    //Detection
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;



    // Animation
    Animator animator;

    //Deals dmg;
    [SerializeField] private bool collided;
    [SerializeField] private EnemyHealth health;


    private void Awake()
    {
        character = GameObject.Find("Character");
        player = character.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        hpAppearInRange = Physics.CheckSphere(transform.position, hpAppearRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (hpAppearInRange) hpBar.SetActive(true);
        else hpBar.SetActive(false);

        if (playerInSightRange) LookPlayer();

        if (collided) DealsDmg();
        if (playerInAttackRange) StartCoroutine(AttackPlayer());

        if (health.isDead)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AttackPlayer()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(1.5f);
        if (!alreadyAttacked)
        {
         //   Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            aimer.LookAt(player);
            //Attack code here
            Rigidbody rb = Instantiate(projectile, aimer.position, aimer.rotation).GetComponent<Rigidbody>();
            rb.velocity = transform.TransformDirection(Vector3.forward * 5);
            rb.AddForce(aimer.forward * 50f, ForceMode.Impulse);
            rb.AddForce(aimer.up * 0, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("isAttack", false);
    }
    private void LookPlayer()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPosition);
        aimer.LookAt(player.position);
    }

    private void DealsDmg()
    {
        character.GetComponent<AshPC>().SetHealth(-.05f);

        if (!collided) character.GetComponent<AshPC>().SetHealth(0f);
        if (character.GetComponent<AshPC>().health <= 0) collided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collided = false;
        }
    }
}
