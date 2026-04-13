using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bench : MonoBehaviour
{
    public bool interacted;
    public bool inRange = false;

    private void Update()
    {
        if(inRange && Input.GetButtonDown("Interact"))
        {
            interacted = true;

            SaveData.Instance.benchSceneName = SceneManager.GetActiveScene().name;
            SaveData.Instance.benchPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            SaveData.Instance.SaveBench();
            //SaveData.Instance.SavePlayerData();
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Player")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Player"))
        {
            interacted = false;
            inRange = false;
        }
    }
}
