using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Interact_RPC()
    {
        EnvironmentSpawner.Instance.OnTake(this.gameObject);
    }

    public void Interact(Character player)
    {
        if (player.PhotonIsMine)
        { 
            player.MyCoins++;
        }
        _photonView.RPC("Interact_RPC", RpcTarget.All);
    }
}
