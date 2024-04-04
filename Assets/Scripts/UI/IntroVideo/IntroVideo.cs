using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class IntroVideo : MonoBehaviour
{
    public VideoPlayer video;
    public AudioSource audio;
    public GameObject pauseWidget;
    public GameObject skipWidget;
    private bool skippable;
    


    public void start()
    {
        pauseWidget.SetActive(false);
        skipWidget.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {
        long frame = video.frame;
        long frameCount = (long)video.frameCount - 10;


        



        if(frame >= frameCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            TogglePaused(true);
            pauseWidget.SetActive(true);
        }
        else if (skippable && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(Input.anyKeyDown)
        {
            skipWidget.SetActive(true);
            skippable = true;
        }
        
        


    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePaused(bool doPause)
    {
        if(doPause)
        {
            video.Pause();
            audio.Pause();
        }
        else
        {
            video.Play();
            audio.Play();
        }
    }



}
