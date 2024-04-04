using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingTree : MonoBehaviour
{
    private float timer = 2;
    public Material burnedMat;
    private MeshRenderer mesh;
    public GameObject poof;
    private bool isBurned;
    public Transform poofPoint;
    public Transform poofPoint2;
    public Transform poofPoint3;
    public Objectives objectives;

    public GameObject fireObj;


    // Start is called before the first frame update
    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBurned)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                
                

                mesh.material = burnedMat;
                gameObject.transform.localScale -= new Vector3(.025f, 0f, .025f);

                if (transform.localScale.x <= 0)
                {

                    GameObject poof2 = Instantiate(poof, poofPoint.position, poofPoint.rotation);
                    GameObject poof3 = Instantiate(poof, poofPoint2.position, poofPoint2.rotation);
                    GameObject poof4 = Instantiate(poof, poofPoint3.position, poofPoint3.rotation);
                    Destroy(gameObject);
                    objectives.UpdateObjText("Continue across the bridge and relight the Totem flames");
                }
                


            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
     if(other.tag == "Fireball")
        {
            ToggleBurned();
        }
    }

    


    public void ToggleBurned()
    {

        fireObj.SetActive(true);
        isBurned = true;
    }
}
