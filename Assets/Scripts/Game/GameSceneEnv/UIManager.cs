using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private CanvasGroup _onDeadCanvasGroup;
    [SerializeField]
    private Text _results;
    [SerializeField]
    private Text _playerStatus;

    private void Awake()
    {
        Instance = this;
    }

    public void OnDead(int coinsCount)
    {
        OpenDeadMenu();
        ShowTextResults(coinsCount);
    }

    private void ShowTextResults(int coinsCount)
    {
        _results.text = string.Format("Coins: {0}", coinsCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            _playerStatus.text = string.Format("Win");
        }
        else
        {
            _playerStatus.text = string.Format("Lose");
        }
    }
    private void OpenDeadMenu()
    {
        _onDeadCanvasGroup.blocksRaycasts = true;
        _onDeadCanvasGroup.alpha = 1;
    }
}
