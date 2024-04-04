using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    public AudioSource audio;
    private float volume;
    private float currentTime = 0;
    private float duration = 2f;
    private bool isFadingIn;
    private bool isFadingOut;

    public void CallFadeIn()
    {
        isFadingIn = true;
        
    }



    public void Update()
    {
       
        if(isFadingIn)
        {
            currentTime += Time.deltaTime;
            if (currentTime < duration)
            {
                audio.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
                
            }
            else
            {
                isFadingIn = false;
                currentTime = 0;
                
            }
        }

        if(isFadingOut)
        {
            currentTime += Time.deltaTime;
            if (currentTime < duration)
            {
                audio.volume = Mathf.Lerp(1f, 0f, currentTime / duration);
                
            }
            else
            {
                isFadingOut = false;
                currentTime = 0;
                
            }
        }
    }    

    public void CallFadeOut()
    {
        isFadingOut = true;
        
    }

    private void FadeOut()
    {
    }

}
