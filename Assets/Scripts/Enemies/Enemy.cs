using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    
    [field: SerializeField]
    public int Health { get; private set; }
    
    [field: SerializeField]
    public float Speed { get; private set; }
    
    
    private Animator _animController;
    [SerializeField] private GameObject BaseArea;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _reachedTopOfWall = false, _reachedExitArea = false;
    private bool _isAlive = true;
    [SerializeField] private float rayDistance = 0.1f;

    public UnityEvent onDie;
     
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animController = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
        
        if (_isAlive)
        {
            if (!_reachedTopOfWall && !_reachedExitArea)
            {
                MoveToDestination();
            }
            else if (_reachedTopOfWall && !_reachedExitArea)
            {
                _navMeshAgent.SetDestination(BaseArea.transform.position);
            }
        }
        ShootRaycast();

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
        onDie?.Invoke();
        _isAlive = false;
        _rigidbody.AddForce(Vector3.down * 15f, ForceMode.Impulse);
        _animController.SetBool("HasDied", true);
        _navMeshAgent.enabled = false;
        Destroy(gameObject, 2.5f);
    }

    private void MoveToDestination()
    {
        _navMeshAgent.SetDestination(_target.position);
        _animController.SetFloat("MovementSpeed" ,Speed);
        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _navMeshAgent.stoppingDistance)
        {
            _reachedTopOfWall = true;
        }
    }
    
    // Maximum distance the Raycast will check for collisions
    private void ShootRaycast()
    {
        // Define the origin of the Raycast (the position of the GameObject)
        Vector3 origin = transform.position;

        // Define the direction of the Raycast (the forward direction of the GameObject)
        Vector3 direction = transform.forward;

        // Shoot the Raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                // If the Raycast hits the wall start climbing animation
                _animController.SetBool("IsClimbing", true);

            }
        }
        else
        {
            // If the Raycast does not hit anything, log that no hit was detected
            //Debug.Log("No hit detected");
        }
    }
    
}
