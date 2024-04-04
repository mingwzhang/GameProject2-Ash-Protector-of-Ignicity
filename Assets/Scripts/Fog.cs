using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private GameObject character;

    private bool caught;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (caught) takeDmg();

        //the player will not take dmg if he died by trap and reset the game (To fix a bug where the player died to trap and will still take dmg).
        if (character.GetComponent<AshPC>().health <= 0) caught = false;
    }

    private void takeDmg()
    {
        // player takes dmg when he touched the trap
        character.GetComponent<AshPC>().SetHealth(-100f);

        // player stop taking dmg when he got out of the trap
        if (!caught) noDmg();
    }

    private void noDmg()
    {
        character.GetComponent<AshPC>().SetHealth(0f);
    }

    // Trigger colldiers conditions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            caught = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            caught = false;
        }
    }
}
