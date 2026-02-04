using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private float cameraLeftStop;
	[SerializeField] private float cameraRightStop;
	[SerializeField] private float cameraTopStop;
	[SerializeField] private float cameraBottomStop;
	public Rigidbody2D rigidbody;

	//Follow player
	private Vector3 targetPoint = Vector3.zero;
	[SerializeField] private Transform player;
	[SerializeField] private float aheadDistance;
	[SerializeField] private float cameraSpeed;
	private float lookOffset;

	private void Start()
	{
		targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}

	private void Awake()
    {
        rigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
		if (rigidbody.linearVelocity.x > 0f)
		{
			lookOffset = Mathf.Lerp(lookOffset, aheadDistance, cameraSpeed * Time.deltaTime);
		}

		if (rigidbody.linearVelocity.x < 0f)
		{
			lookOffset = Mathf.Lerp(lookOffset, -aheadDistance, cameraSpeed * Time.deltaTime);
		}

		//Follow player
		targetPoint.x = player.transform.position.x + lookOffset;
		targetPoint.y = player.transform.position.y;

		if (targetPoint.x <= cameraLeftStop)
		{
			targetPoint.x = cameraLeftStop;
		}

		if (targetPoint.x >= cameraRightStop)
		{
			targetPoint.x = cameraRightStop;
		}

		if (targetPoint.y <= cameraBottomStop)
		{
			targetPoint.y = cameraBottomStop;
		}

		if (targetPoint.y >= cameraTopStop)
		{
			targetPoint.y = cameraTopStop;
		}

		transform.position = Vector3.Lerp(transform.position, targetPoint, cameraSpeed * Time.deltaTime);
	}
}
