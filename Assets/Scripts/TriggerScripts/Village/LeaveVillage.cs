using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveVillage : MonoBehaviour
{
    public Animator fadeOut;
    private float timer = 4;
    private bool isSwitchingLevels;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSwitchingLevels)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }    
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            /*other.GetComponent<AshPC>().canMove = false;
            other.GetComponent<AshPC>().anim.SetFloat("MoveForward", 0f);
            other.GetComponent<AshPC>().anim.SetFloat("MoveRight", 0f);
            */
            fadeOut.SetBool("isLevelCompleted", true);
            isSwitchingLevels = true;
        }
    }

}
