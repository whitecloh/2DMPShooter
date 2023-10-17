using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    [SerializeField]
    private Image _healthsImage;
    [SerializeField]
    private Text _coinsTxt;
    [SerializeField]
    private Text _playerName;

    public Image HealthsImage => _healthsImage;
    public Text Coins
    {
        get
        {
            return _coinsTxt;
        }
        set
        {
            _coinsTxt = value;
        }
    }
    public Text PlayerName
    {
        get
        {
            return _playerName;
        }
        set
        {
            _playerName = value;
        }
    }

    public void UpdateText(string value , Text text)
    {
        text.text = value;
    }
    public void UpdateFill(float value,Image image)
    {
        image.fillAmount = value;
    }
}
