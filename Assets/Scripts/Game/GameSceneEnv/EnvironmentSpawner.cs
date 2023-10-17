using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public static EnvironmentSpawner Instance;

    [SerializeField]
    private Aptechka _aptechkaPrefab;
    [SerializeField]
    private Coin _coinPrefab;
    [SerializeField]
    private Transform[] _aptechkaSpawns;
    [SerializeField]
    private Transform[] _coinSpawns;
    [SerializeField]
    private float _respawnTime;

    private List<Aptechka> _aptechkaList;
    private List<Coin> _coinsList;

    private void Awake()
    {
        Instance = this;
        _aptechkaList = new List<Aptechka>();
        _coinsList = new List<Coin>();
    }
    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for(int i=0;i<_aptechkaSpawns.Length;i++)
        {
            var aptechka = Instantiate(_aptechkaPrefab, _aptechkaSpawns[i]);
            aptechka.GetComponent<PhotonView>().ViewID = 400 + i;
            _aptechkaList.Add(aptechka);
        }

        for (int i = 0; i < _coinSpawns.Length; i++)
        {
            var coin = Instantiate(_coinPrefab, _coinSpawns[i]);
            coin.GetComponent<PhotonView>().ViewID = 500 + i;
            _coinsList.Add(coin);
        }
    }

    public void OnTake(GameObject taken)
    {
        StartCoroutine(Respawn(taken));
    }
    
    private IEnumerator Respawn(GameObject taken)
    {
        taken.SetActive(false);
        yield return new WaitForSeconds(_respawnTime);
        taken.SetActive(true);
    }


}
