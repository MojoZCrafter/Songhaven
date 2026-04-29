using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    [SerializeField] private GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerStay2D(Collider2D _other)
    {
        if(_other.CompareTag("Player"))
        {
            Debug.Log("Detected Nail Swing");
        }
        
    }
}
