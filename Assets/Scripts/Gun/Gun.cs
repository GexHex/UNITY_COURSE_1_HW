using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform spawnPoint;
    public int CanShoot;

    void Update()
    {
        if (CanShoot == 1)
        {
            Shoot();
            CanShoot = 0;
        }
    }

    public void Shoot()
    {
        Instantiate(_bullet, spawnPoint.position, spawnPoint.rotation);
    }
}