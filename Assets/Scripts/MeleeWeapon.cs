using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour
{
  public UnityEvent onCollision;
  public int Damage { get; set; }
  
  private void OnCollisionEnter(Collision other)
  {
    onCollision?.Invoke();
    var enemy = other.gameObject.GetComponent<Enemy>(); 
    if (enemy)
    {
      enemy.TakeDamage(Damage);
    }
  }
}
