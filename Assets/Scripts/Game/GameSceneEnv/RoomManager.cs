using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [SerializeField]
    private Transform[] _spawnPosition;

    public Transform[] SpawnPositions=>_spawnPosition;


    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene ,LoadSceneMode loadMode)
    {
        if(scene.buildIndex==1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"),Vector3.zero,Quaternion.identity);
        }
    }

    public void CheckEndGameState()
    {
        Debug.Log("check");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            var player = FindObjectOfType<Character>();
            Debug.Log(player.GetComponent<PhotonView>().ViewID);
            player.MyPlayerManager.Die();
        }
    }
    public void LeaveRoom()
    {
        Debug.Log("leave");
        PhotonNetwork.LeaveRoom();
    }
    public void Disconnect()
    {
        SceneManager.LoadScene(0);
    }
    public void DestroyOnLeave(GameObject player)
    {
        PhotonNetwork.Destroy(player);
    }

}
