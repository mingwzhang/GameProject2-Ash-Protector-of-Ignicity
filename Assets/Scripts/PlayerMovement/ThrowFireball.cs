using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireball : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject projectile;
    public GameObject camPivot;
    public float velocity = 700f;
    private Vector3 projectileSpawnDir;
    public Transform camFireballTarget;

    public Camera pcCamera;

    public GameObject pcChar;


    public void SpawnFireball()
    {
        RaycastHit hit;
        if (Physics.Raycast(camPivot.transform.position, pcCamera.transform.forward, out hit))
        {

            //Debug.Log(hit.distance);
            if(!hit.collider.CompareTag("Player") && hit.distance < 9f)
            {
                spawnPoint.transform.LookAt(hit.point);
            }
            else if(!hit.collider.CompareTag("Player") && hit.distance >= 9)
            {
                spawnPoint.transform.LookAt(hit.point + new Vector3(0f , hit.distance * .05f, 0f));
            }

            //Vector3 temp = pcCamera.transform.forward;
            //Debug.Log(temp);
            //temp.y = temp.y+0.2f;
            
        }
        else
        {
            spawnPoint.transform.LookAt(camFireballTarget);
        }
        GameObject ball = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, velocity));
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 //       Debug.DrawRay(spawnPoint.transform.position, transform.forward * 50, Color.green);

    }
}
