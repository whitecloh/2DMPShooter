using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    private Character _player;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(_photonView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        _player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity, 0 ,new object[] { _photonView.ViewID}).GetComponent<Character>();
        _player.GetComponent<PhotonView>().RPC("SetPosition",RpcTarget.All,PhotonNetwork.LocalPlayer.GetPlayerNumber());
        _player.GetComponent<PhotonView>().RPC("SetSkin", RpcTarget.All, PhotonNetwork.LocalPlayer.GetPlayerNumber());
        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.All);
        CameraController.Instance.SetTarget(_player);
    }

    public void Die()
    {
        UIManager.Instance.OnDead(_player.MyCoins);
        PhotonNetwork.Destroy(_player.gameObject);
        RoomManager.Instance.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(!_photonView.IsMine)
        {
            RoomManager.Instance.CheckEndGameState();
        }
    }
}
