using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkeletonAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform player;

    [SerializeField] private GameObject hpBar;
    [SerializeField] private LayerMask whatIsPlayer;
    

    public AudioSource deathSource;
    public AudioClip deathClip;
    public float volume = 0.5f;
 
    // Detection
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool hpAppearInRange;

    [SerializeField] private float sightRange;
    [SerializeField] private float hpAppearRange;


    // Animation
    Animator animator;

    
    [SerializeField] private EnemyHealth health;


    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
     }
 
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        hpAppearInRange = Physics.CheckSphere(transform.position, hpAppearRange, whatIsPlayer);

        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        if (playerInSightRange) ChasePlayer();

        if (hpAppearInRange) hpBar.SetActive(true);
        else hpBar.SetActive(false);


        

        if(health.isDead)
        {
            animator.SetTrigger("isDead");  
            agent.isStopped = true;
            hpBar.SetActive(false);
            Destroy(gameObject, 2.5f);
        }
    }

    private void death(){
        deathSource.Play();
    } 
    private void ChasePlayer()
    {
        animator.SetBool("follow", true);
        agent.SetDestination(player.position);
    }

    

    
}
