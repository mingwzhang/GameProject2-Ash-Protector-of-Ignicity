using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    public GameObject pauseWidget;
    public Text skipWidget;
    public bool skippable;

    private void Start()
    {
        
        skipWidget.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                TogglePaused(true);
                pauseWidget.SetActive(true);
            }
        else if (skippable && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            LoadMainMenu();
        }
        else if (Input.anyKeyDown)
        {
            skipWidget.enabled = true;
            skippable = true;
        }
    }

    public void LoadMainMenu()
    {
            SceneManager.LoadScene(0);

    }

    public void TogglePaused(bool doPause)
    {
        if(doPause)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}
