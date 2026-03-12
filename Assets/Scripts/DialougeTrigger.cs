using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueBoxUI;
    public PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dialogueBoxUI != null)
        {
            dialogueBoxUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueBoxUI != null)
                dialogueBoxUI.SetActive(true);
            if (playerController != null)
                playerController.isFrozen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueBoxUI != null)
                dialogueBoxUI.SetActive(false);
                 if (playerController != null)
                playerController.isFrozen = false;
        }
    }
}
