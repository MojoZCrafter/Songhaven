using UnityEngine;

public class WholeNote : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player"))
        {
            PlayerController.Instance.Heal();
            AudioManager.Instance.PlaySFX("Heal");
            Destroy(gameObject);
        }
    }
}
