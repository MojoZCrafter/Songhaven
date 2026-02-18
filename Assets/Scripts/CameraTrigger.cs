using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera newCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
            CameraManager.Instance.SwapCamera(newCamera);
        }
    }
}
