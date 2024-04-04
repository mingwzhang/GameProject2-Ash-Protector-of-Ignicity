using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesStopSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StopSpawn());
    }

    IEnumerator StopSpawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
