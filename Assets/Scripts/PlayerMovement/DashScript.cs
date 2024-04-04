using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashScript : MonoBehaviour
{
    AshPC playerMove;
    public GameObject ashMesh;
    public Animator ashAnim;
    public TrailRenderer trail;

    public float dashSpeed;
    public Slider slider;
    float timerLength = 1.5f;
    float ttime = 1.5f;
    int count = 0;
    public float dashTime;
    // Start is called before the first frame update
    void Start()
    {
        //set slider values
        slider.maxValue = timerLength;
        slider.value = timerLength;

        playerMove = GetComponent<AshPC>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Dash")&& count == 0f)
        {
            StartCoroutine(Dash());
            count=1;
            ttime = 0;
        }
        if(count!=0){
        //starts cooldown
        ttime = ttime + (Time.deltaTime);
        slider.value = ttime;
        //resets slider
            if(slider.value == 1.5f){
                count = 0;
 //               slider.value = 2f;
//                ttime = 2f;
            }
        }

    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        ashAnim.SetBool("isDashing", true);
        trail.emitting = true;
        while (Time.time < startTime + dashTime)
        {
            
            playerMove.controller.Move(ashMesh.transform.forward * dashSpeed);
            yield return null;
        }
        if (Time.time > startTime + dashTime)
        {
            ashAnim.SetBool("isDashing", false);
            trail.emitting = false;
        }
    }
}
