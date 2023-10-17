using Photon.Pun;
using System.Collections;
using System.IO;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField]
    private ProjectileController _bullet;
    [SerializeField]
    private float _shootingDelay;
    [SerializeField]
    private Transform _shootPosition;

    private bool isReady=true;

    public void OnShoot()
    {
        if (isReady)
        {
            StartCoroutine(ShootingDelayCorroutine());
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Bullet"), _shootPosition.position, transform.rotation);
        }
    }

        private IEnumerator ShootingDelayCorroutine()
    {
        isReady = false;
        yield return new WaitForSeconds(_shootingDelay);
        isReady = true;
    }
}
