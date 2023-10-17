using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    public static BulletsPool Instance;

    [SerializeField]
    private ProjectileController _bullet;
    [SerializeField]
    private int _amountBulletsToPool;

    private List<GameObject> _bullets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < _amountBulletsToPool; i++)
        {
            tmp = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Bullet"),Vector3.zero,Quaternion.identity);
            tmp.transform.SetParent(transform);
            tmp.SetActive(false);
            _bullets.Add(tmp);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < _amountBulletsToPool; i++)
        {
            if (!_bullets[i].activeInHierarchy)
            {             
                _bullets[i].GetComponent<PhotonView>().RPC("ShowBullet", RpcTarget.All);
                return _bullets[i];
            }
        }
        return null;
    }
}
