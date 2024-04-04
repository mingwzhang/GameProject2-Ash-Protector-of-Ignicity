using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject invWall;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            invWall.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

}
