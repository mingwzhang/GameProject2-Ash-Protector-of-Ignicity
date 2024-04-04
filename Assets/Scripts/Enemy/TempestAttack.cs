using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempestAttack : MonoBehaviour
{
    public Tempest tempest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CallAttack()
    {
        tempest.SpawnRainCloud();
    }

    public void AttackStart()
    {
        tempest.ToggleAttacking(true);
    }

    public void AttackEnd()
    {
        tempest.ToggleAttacking(false);
    }

}
