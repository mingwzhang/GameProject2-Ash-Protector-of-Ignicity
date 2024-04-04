using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject pauseMenuWidget;
    private GameObject optionsWidget;
    public GameObject backButton;
    public GameObject resumeButton;
    private GameObject confirmWidget;
    public GameObject cancelButton;
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;

    

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        eventSystem = EventSystem.FindObjectOfType<EventSystem>();
        pauseMenuWidget = GameObject.Find("PauseWidget");
        optionsWidget = GameObject.Find("OptionsMenuWidget");
        confirmWidget = GameObject.Find("ConfirmWidget");
        
        //gameObject.SetActive(false);
        optionsWidget.SetActive(false);
        confirmWidget.SetActive(false);

        //gets and sets resolutions
        if(Screen.resolutions != null)
        {
            resolutions = Screen.resolutions;

        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions();

        }
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i ++){
            string option = resolutions[i].width + "x"+ resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width==Screen.currentResolution.width &&
            resolutions[i].height==Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
        }
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   

    

    public void ToggleOptions()
    {
        if (pauseMenuWidget.activeSelf == true)
        {
            pauseMenuWidget.SetActive(false);
            optionsWidget.SetActive(true);
            eventSystem.SetSelectedGameObject(backButton);
        }
        else
        {
            optionsWidget.SetActive(false);
            pauseMenuWidget.SetActive(true);
            eventSystem.SetSelectedGameObject(resumeButton);
        }
    }

    public void ToggleConfirmWidget()
    {
        if(confirmWidget.activeSelf == false)
        {
            confirmWidget.SetActive(true);
            pauseMenuWidget.SetActive(false);
            eventSystem.SetSelectedGameObject(cancelButton);
        }
        else
        {
            confirmWidget.SetActive(false);
            pauseMenuWidget.SetActive(true);
            eventSystem.SetSelectedGameObject(resumeButton);
        }
    }
    
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        AshPC.isPaused = false;
    }
    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
}
