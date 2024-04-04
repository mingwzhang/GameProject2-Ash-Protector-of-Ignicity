using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconScaler : MonoBehaviour
{
    public GameObject[] Beacons;        //Array of beacons
    public Vector3[] beaconScales;      //Scale Vector3s of the nth Beacon in Beacons
    public Vector3[] beaconpositions;   //Position Vector3s of the nth Beacon in Beacons
    public AshPC player;
    public Vector3 currentplayerPosition;
    public GameObject playerCharacter;
    private Vector3[] BeaconDistfromPlayer; //Subtracted difference between nth position of beaconpositions and the currentplayerPosition
    private Vector3 beaconScaleMultiplier;  //Multiplier for the beacon scales, calculated from the multiplication of beaconScales[i] and BeaconDistfromPlayer[i]
    
    // Start is called before the first frame update
    void Start()
    {
        Beacons = GameObject.FindGameObjectsWithTag("Beacon"); 
        for (int i = 0; i < Beacons.Length; i++)
        {
            beaconpositions[i] = Beacons[i].transform.position;
        } 
        
        for (int i = 0; i < Beacons.Length; i++)
        {
            beaconScales[i] = Beacons[i].transform.localScale;
        }
    }

    //Update is called once per frame
    void Update()
    { 
        currentplayerPosition = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, playerCharacter.transform.position.z);
        for (int i = 0; i < Beacons.Length; i++)
        {
            beaconpositions[i] = new Vector3(Beacons[i].transform.position.x, Beacons[i].transform.position.y, Beacons[i].transform.position.z);
            BeaconDistfromPlayer[i] = beaconpositions[i] - currentplayerPosition;
        }
        
        
    }

    public void ScaleBeacon()
    {
        for (int i = 0; i < Beacons.Length; i++)
        {
            beaconScales[i] = new Vector3(beaconScales[i].x, beaconScales[i].y, beaconScales[i].z);
            BeaconDistfromPlayer[i] = new Vector3(BeaconDistfromPlayer[i].x, BeaconDistfromPlayer[i].y, BeaconDistfromPlayer[i].z);

            //beaconScaleMultiplier = Vector3.Dot(beaconScales[i], BeaconDistfromPlayer[i]);
        }
    }
}
