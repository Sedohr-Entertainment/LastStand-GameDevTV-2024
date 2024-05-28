using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private int Health { get; set; }
    public float Speed { get; set; }
    
    [SerializeField] private GameObject BaseArea;
    
    [SerializeField] private Transform _target;
    

    [SerializeField] private bool _reachedTopOfWall = false, _reachedExitArea = false;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
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

    private void MoveToDestination()
    {
        _navMeshAgent.SetDestination(_target.position);
        
        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _navMeshAgent.stoppingDistance)
        {
            _reachedTopOfWall = true;
        }
    }
    
}
