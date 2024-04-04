using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectiveTrigger : MonoBehaviour
{
    public GameObject fire;
    public AshPC player;
    public GameObject interactTextObj;
    public Transform spawnPoint;
    public GameObject tempTextObj;

    private PillarStairTrigger PillarStairTriggers;
    private bool isCaveArea;

    private void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();

        if(GameObject.Find("Pedestral group"))
        {
            isCaveArea = true;
            PillarStairTriggers = GameObject.Find("Pedestral group").GetComponent<PillarStairTrigger>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject spawnedText = Instantiate(interactTextObj, spawnPoint.position, this.transform.rotation);
            tempTextObj = spawnedText;
            other.GetComponent<AshPC>().totemTrigger = this;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(tempTextObj);
        }
    }

    private void Update()
    {
     
        
    }

    public void ToggleTotem()
    {
        fire.SetActive(true);
        gameObject.SetActive(false);
        player.GiveFlameToPedestal();
        player.PlayPickupAudio();
        Debug.Log("Toggle");
        player.UpdateObjective("Pedestal");

        if(isCaveArea)
        {
            PillarStairTriggers.PillarStairCount();
        }
    }







}
