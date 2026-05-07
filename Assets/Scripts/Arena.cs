using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorToUnlock;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2 spawnPointOne;
    [SerializeField] private Vector2 spawnPointTwo;
    private bool arenaActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player") && arenaActive == true)
        {
            door.SetActive(true);
            Instantiate(enemy, spawnPointOne, Quaternion.identity);
            Instantiate(enemy, spawnPointTwo, Quaternion.identity);
            arenaActive = false;
        }
    }
}
