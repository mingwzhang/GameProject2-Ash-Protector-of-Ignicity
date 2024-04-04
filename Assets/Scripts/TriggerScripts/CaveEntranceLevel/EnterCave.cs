using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCave : MonoBehaviour
{
    public Animator fadeWidget;
    private bool isFading;
    public float timer;
    private AshPC player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
            
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fadeWidget.SetBool("isLevelCompleted", true);
            isFading = true;
            player.canMove = false;
            player.anim.SetFloat("MoveForward", 0f);
            player.anim.SetFloat("MoveRight", 0f);
        }
    }




}
