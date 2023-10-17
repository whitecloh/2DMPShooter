using Photon.Pun;
using UnityEngine;

public class Aptechka : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _healValue;

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
        player.GetComponent<HealthComponent>().TakeHeal(_healValue);
        _photonView.RPC("Interact_RPC", RpcTarget.All);
    }
}
