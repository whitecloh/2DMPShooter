using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private UIComponent _uiComponent;
    [SerializeField]
    private SkinComponent _skin;

    private PlayerManager playerManager;
    public PlayerManager MyPlayerManager => playerManager;

    private int _coins;
    private float _healths = 0;
    private float _maxHealths;
    public int MyCoins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
            _uiComponent.UpdateText(_coins.ToString(), _uiComponent.Coins);
        }
    }
    public float MyHealths
    {
        get
        {
            return _healths;
        }
        set
        {
            _healths = value;
            _uiComponent.UpdateFill(_healths / _maxHealths, _uiComponent.HealthsImage);
        }
    }
    public float MyMaxHealths
    {
        get
        {
            return _maxHealths;
        }
        set
        {
            _maxHealths = value;
        }
    }

    public bool PhotonIsMine
    {
        get
        { 
            return _photonView.IsMine; 
        }
        private set { }
    }

    private void Awake()
    {
        playerManager = PhotonView.Find((int)_photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        if(!PhotonIsMine)
        {
            _uiComponent.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void SetPosition(int i)
    {
            transform.SetParent(RoomManager.Instance.SpawnPositions[i]);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
    }
    [PunRPC]
    public void SetSkin(int i)
    {
            _skin.CreateSkin(i);
    }
    [PunRPC]
    public void SetNickName()
    {
            _uiComponent.UpdateText(_photonView.Owner.NickName, _uiComponent.PlayerName);
    }

}
