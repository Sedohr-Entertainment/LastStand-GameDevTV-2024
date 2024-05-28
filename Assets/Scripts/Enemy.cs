using System;
using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private int Health { get; set; }

    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private bool _wallReached = false;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToDestination();
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO Kill enemy and Play death animation
        Destroy(gameObject);
    }

    private void StartClimb()
    {
        
    }

    private void MoveToDestination()
    {
        _navMeshAgent.SetDestination(_target.position);
    }
    

}
