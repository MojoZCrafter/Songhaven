using UnityEngine;

public class DestroysUI : MonoBehaviour
{
    public static DestroysUI Instance;
    public GameObject uiPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}