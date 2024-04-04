using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimParameters : MonoBehaviour
{
    private Animator anim;
    public AshPC player;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void ResetDoubleJumpBool()
    {
        anim.SetBool("doubleJumping", false);
    }    

    public void ResetIsPunching()
    {
        player.canMove = true;
        anim.SetBool("isPunching", false);
    }




}
