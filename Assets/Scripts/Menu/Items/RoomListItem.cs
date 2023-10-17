using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    public RoomInfo _info;

    public void SetUp(RoomInfo info)
    {
        _info = info; 
        _text.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(_info);
    }
}
