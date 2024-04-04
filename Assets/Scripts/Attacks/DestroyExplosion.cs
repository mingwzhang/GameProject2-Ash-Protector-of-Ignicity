using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    private float timer = 1;
    
    private AudioSource sound;



    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        sound.pitch = Random.Range(.7f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyParent()
    {
        Destroy(gameObject);
    }

}
