using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFlyover : MonoBehaviour
{
    public GameObject FlyoverCamera;
    public GameObject MainCamera;
    private Animator flyoverFadeAnim;
    private AshPC player;
    private bool fadeIsCalled;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<AshPC>();
        flyoverFadeAnim = GameObject.Find("Flyover Assets/FlyoverFadeCanvas/FlyoverFadeAnim").GetComponent<Animator>();
    }

    public void Flyover()
    {
        FlyoverCamera.SetActive(true);
        MainCamera.SetActive(false);
    }

    public void MainCamToggle()
    {
        MainCamera.SetActive(true);
        FlyoverCamera.SetActive(false);
    }

    public void ToggleCanMove()
    {
        player.SetCanMove(true);
    }

    public void ToggleCantMove()
    {
        player.SetCanMove(false);    
    }

    public void CallFadeAnim()
    {
        flyoverFadeAnim.SetBool("fadeIsCalled", true);
        Debug.Log("CallFadeAnim called.");
    }

    public void DisableFadeAnim()
    {
        flyoverFadeAnim.SetBool("fadeIsCalled", false);
        Debug.Log("DisableFadeAnim called.");
    }
}
