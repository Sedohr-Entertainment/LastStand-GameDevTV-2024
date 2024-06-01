using System;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Test");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject}");
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            //TODO add game over condition
            Debug.Log("Game Over!");
        }    
    }
}
