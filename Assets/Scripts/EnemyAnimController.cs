using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimController : MonoBehaviour
{
    
    private Animator _animController;
   
    private void Awake()
    {
        _animController = GetComponent<Animator>();
        
    }

    public void SetFloat(float movementSpeed)
    {
        _animController.SetFloat("MovementSpeed", movementSpeed );
    }
    public void SetBool()
    {
        _animController.SetBool("IsClimbing", true );
    }


}
