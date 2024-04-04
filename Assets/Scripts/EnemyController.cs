using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private GameObject character;

    // Update is called once per frame
    void Update()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        target = character.GetComponent<Transform>();
        agent.SetDestination(target.position);
        float dist = Vector3.Distance(agent.transform.position, target.position);
        float minDist = 7.0f;
        if(dist < minDist)
        {
            character.GetComponent<AshPC>().SetHealth(-0.05f);
        }
        
    }
}
