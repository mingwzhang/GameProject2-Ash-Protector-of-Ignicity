using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject menuWidget;
    public GameObject optionsWidget;
    public GameObject backButton;
    public GameObject startButton;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        //eventSystem = EventSystem.current;
        //menuWidget = GameObject.Find("MainMenuWidget");
        //optionsWidget = GameObject.Find("OptionsMenuWidget");
        
        optionsWidget.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    
    public void QuitGame()
    {
        /*if(UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {

        }*/
        Application.Quit();
    }

    public void ToggleOptions()
    {
        if(menuWidget.activeSelf == true)
        {
            menuWidget.SetActive(false);
            optionsWidget.SetActive(true);
            eventSystem.SetSelectedGameObject(backButton);
        }
        else
        {
            optionsWidget.SetActive(false);
            menuWidget.SetActive(true);
            eventSystem.SetSelectedGameObject(startButton);
        }
    }


}
