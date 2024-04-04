using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private Vector3 upperFloor ;
    [SerializeField] private Vector3 bottomFloor ;  

    //   short cut Vector3(517, 201, 651) to 580,3, 580;

    public GameObject trigger;
    [SerializeField] private bool isAtTop = false;

    [SerializeField] private bool isTriggered;

    private void Update()
    {
        if (isTriggered && !isAtTop)
        {
            MoveUp();
        }

        if (isTriggered && isAtTop)
        {
            MoveDown();
        }

        if (trigger.transform.position == upperFloor)
        {
            isTriggered = false;
            isAtTop = true;
        }
        if (trigger.transform.position == bottomFloor)
        {
            isTriggered = false;
            isAtTop = false;
        }
    }

    private void MoveUp()
    {
        trigger.transform.position = Vector3.MoveTowards(trigger.transform.position, upperFloor, 50 * Time.deltaTime);

    }

    private void MoveDown()
    {
        trigger.transform.position = Vector3.MoveTowards(trigger.transform.position, bottomFloor, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isTriggered = true;
            player.transform.parent = transform;
        }
    }
 
}
