using Photon.Pun;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _maxHealths;
    private float _currentHealths;
    private Character _player;

    public float MaxHealths => _maxHealths;

    private void Awake()
    {
        _player = GetComponent<Character>();
    }
    private void Start()
    {
        Initialize();
    }
    public void TakeDamage(int damage)
    {
        _player.GetComponent<PhotonView>().RPC("OnTakeDamage", RpcTarget.All, damage);
    }
    public void TakeHeal(int heal)
    {
        _player.GetComponent<PhotonView>().RPC("OnTakeHeal", RpcTarget.All, heal);
    }

    [PunRPC]
    public void OnTakeDamage(int damage)
    {
        if (!_player.PhotonIsMine) return;
        
            _currentHealths -= damage;
            _player.MyHealths = Mathf.Clamp(_currentHealths, 0, _maxHealths);
            _currentHealths = _player.MyHealths;
        
        if (_player.MyHealths <= 0)
        {
            OnDead();
        }
    }
    [PunRPC]
    public void OnTakeHeal(int heal)
    {
        if (!_player.PhotonIsMine) return;

        _currentHealths += heal;
        _player.MyHealths = Mathf.Clamp(_currentHealths, 0, _maxHealths);
        _currentHealths = _player.MyHealths;
    }

    private void OnDead()
    {
        _player.MyPlayerManager.Die();
    }
    private void Initialize()
    {
        _currentHealths = _maxHealths;
        _player.MyHealths = _currentHealths;
        _player.MyMaxHealths = _maxHealths;
    }

}
