using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimController : MonoBehaviour
{
    
    private Animator _animController;
    private Enemy _enemy;
    
    private void Awake()
    {
        _animController = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        
    }

    private void SetAnimation()
    {
        _animController.SetFloat("", _enemy.Speed );
    }


}
