using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform spawnPoint;

    public void Shoot()
    {
        Instantiate(_bullet, spawnPoint.position, spawnPoint.rotation);
    }
}