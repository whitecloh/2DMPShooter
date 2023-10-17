using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField]
    private InputField _roomNameInputField;
    [SerializeField]
    private Text _errorText;
    [SerializeField]
    private Text _roomName;
    [SerializeField]
    private Transform _roomListContent;
    [SerializeField]
    private Transform _playerListContent;
    [SerializeField]
    private GameObject _roomListItemPrefab;
    [SerializeField]
    private GameObject _playerListItemPrefab;
    [SerializeField]
    private GameObject _startGameButton;
    [SerializeField]
    private GameObject _deleteRoomButton;
    [SerializeField]
    private InputField _joinRoomInput;
    [SerializeField]
    private Text _waitingText;
    [SerializeField]
    private int _maxPlayersInRoom=3;

    private Player[] _players;
    private List<RoomInfo> _fullRoomList;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        _fullRoomList = new List<RoomInfo>();
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        _roomName.text = PhotonNetwork.CurrentRoom.Name;

        _players = PhotonNetwork.PlayerList;

        foreach(Transform tmp in _playerListContent)
        {
            Destroy(tmp.gameObject);
        }

        for (int i = 0; i < _players.Length; i++)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(_players[i],i);
        }
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        _deleteRoomButton.SetActive(true);

        _waitingText.text = string.Format("Waiting ... {0}/{1}", PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        _players = PhotonNetwork.PlayerList;
        _deleteRoomButton.SetActive(true);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room Creation Failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Join Room Failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in _roomListContent)
        {
            Destroy(trans.gameObject);

        }

        for (int i = 0; i <= roomList.Count - 1; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                for (int a = 0; a < _fullRoomList.Count; a++)
                {
                    if (_fullRoomList[a].Name.Equals(roomList[i].Name)) _fullRoomList.RemoveAt(a);
                }
            }
            if (!_fullRoomList.Contains(roomList[i])) _fullRoomList.Add(roomList[i]);

            for (int b = 0; b < _fullRoomList.Count; b++)
            {
                if (_fullRoomList[b].Name.Equals(roomList[i].Name)) _fullRoomList[b] = roomList[i];
            }
        }

        if (!(_fullRoomList.Count == 0))
        {
            for (int i = 0; i < _fullRoomList.Count; i++)
            {
                if (_fullRoomList[i].RemovedFromList == false)
                {
                    Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(_fullRoomList[i]);
                }
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _waitingText.text = string.Format("Waiting ... {0}/{1}", PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);

        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer,_players.Length);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text))
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayersInRoom;

        PhotonNetwork.CreateRoom(_roomNameInputField.text,roomOptions,TypedLobby.Default);
        MenuManager.Instance.OpenMenu("loading");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
    public void JoinRoomByInput()
    {
        if(_joinRoomInput.text!="")
        PhotonNetwork.JoinRoom(_joinRoomInput.text);
    }
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            _waitingText.text = string.Format("Room not full!!");
            return;
        }
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(1);
    }
}
