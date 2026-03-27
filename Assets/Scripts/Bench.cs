using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    public bool interacted;
    public bool inRange = false;

    private void Update()
    {
        if(inRange && Input.GetButtonDown("Interact"))
        {
            interacted = true;
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Player")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
