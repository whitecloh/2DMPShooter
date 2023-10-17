using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField]
    private InputField _playerName;

    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            _playerName.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = _playerName.text;
        }
        else
        {
            _playerName.text = "Player" + Random.Range(0, 10000).ToString("0000");
            OnUserNameChanged();
        }
    }
    public void OnUserNameChanged()
    {
        PhotonNetwork.NickName = _playerName.text;
        PlayerPrefs.SetString("username", _playerName.text);
    }
}
