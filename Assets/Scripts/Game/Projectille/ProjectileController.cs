using UnityEngine;

    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private int _damage;

        private void Update()
        {
            ProjectileMovement();
        }

        private void ProjectileMovement()
        {
            transform.position += transform.up * _moveSpeed * Time.deltaTime;
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character>();

        if(player!=null&&player.PhotonIsMine)
        {
                player.GetComponent<HealthComponent>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}