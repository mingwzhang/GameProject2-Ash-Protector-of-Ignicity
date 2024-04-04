using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public AshPC player;
    public Vector3 CheckpointObj;
    public Vector3 spawnPoint;

    void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();
        CheckpointObj = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
        spawnPoint = player.transform.position;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spawnPoint = CheckpointObj;
            Debug.Log("Checkpoint set");
        }
    }
}