using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitArea : MonoBehaviour
{
    public UnityEvent onAreaEntered;
  
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            onAreaEntered?.Invoke();
        }    
    }
}
