using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] float resetTime;
    [SerializeField] string playerTag = "Player";
    [SerializeField] bool canReset;

    public AudioSource crumbling;

    Animator p_anim;
    
    // Start is called before the first frame update
    void Start()
    {
        p_anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            p_anim.SetBool("Trigger", true);
            Debug.Log("Trigger Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            p_anim.SetBool("Trigger", false);
            Debug.Log("Trigger Exit");
        }
    }

    public void TriggerReset()
    {
        if(canReset)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        p_anim.SetBool("Trigger", false);
    }
    private void crumblingSound(){
       crumbling.Play();
    }
}
