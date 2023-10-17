using UnityEngine;

public class MoveComponent : MonoBehaviour, IControllable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rb;

    private Character _player;

    private Vector2 _moveDirection;

    private void Awake()
    {
        _player = GetComponent<Character>();

        if(!_player.PhotonIsMine)
        {
            Destroy(_rb);
        }
    }
    public void Move()
    {
        _rb.MovePosition(_rb.position + _moveDirection * Time.fixedDeltaTime);
    }

    public void Rotation(Vector2 direction)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        _moveDirection = direction * _speed;
    }
}
