using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWallJump : MonoBehaviour
{
    bool used;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerController.Instance.unlockedWallJump)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Player") && !used)
        {
            used = true;
            PlayerController.Instance.unlockedWallJump = true;

            Destroy(gameObject);
        }
    }
}
