using UnityEngine;

public class SkinComponent : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _heads;
    [SerializeField]
    private Sprite[] _equip;
    [SerializeField]
    private Sprite[] _leftHand;
    [SerializeField]
    private Sprite[] _rightHand;

    [SerializeField]
    private SpriteRenderer _headPlace;
    [SerializeField]
    private SpriteRenderer _equipPlace;
    [SerializeField]
    private SpriteRenderer _leftHandPlace;
    [SerializeField]
    private SpriteRenderer _rightHandPlace;


    public void CreateSkin(int skinNumber)
    {
        _headPlace.sprite = _heads[skinNumber];
        _equipPlace.sprite = _equip[skinNumber];
        _leftHandPlace.sprite = _leftHand[skinNumber];
        _rightHandPlace.sprite = _rightHand[skinNumber];
    }
}
