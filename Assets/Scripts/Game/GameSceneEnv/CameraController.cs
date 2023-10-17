using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;

	[SerializeField]
	private int _cameraOffsetZ;
	[SerializeField]
	private float _lookAheadStrength;
	[SerializeField]
	private Vector3 _defaultPosition = new Vector3(-2, -23, -120);

	private Character _player;

    private void Awake()
    {
		Instance = this;
    }

    void LateUpdate()
	{
		if(_player==null)
        {
			HasNotTarget();
        }
		else
        {
			HasTarget();
        }
	}

	public void SetTarget(Character player)
	{
		_player = player;
	}

	void HasTarget()
    {
		Vector2 playerPos = _player.transform.position;
		Rigidbody2D playerRB = _player.GetComponent<Rigidbody2D>();

		Vector2 newCameraPos = playerPos + playerRB.velocity * _lookAheadStrength;

		float newX = Mathf.Lerp(transform.position.x, newCameraPos.x, 0.1f);
		float newY = Mathf.Lerp(transform.position.y, newCameraPos.y, 0.1f);

		transform.position = new Vector3(newX, newY, -_cameraOffsetZ);
	}
	void HasNotTarget()
    {
		transform.position = _defaultPosition;
    }

}
