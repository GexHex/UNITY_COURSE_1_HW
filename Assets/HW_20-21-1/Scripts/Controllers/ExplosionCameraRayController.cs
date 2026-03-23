using UnityEngine;

public class ExplosionCameraRayController : MonoBehaviour
{
    private GameObject _explosionVFX;
    private float _explosionRadius;

    public ExplosionCameraRayController(GameObject explosionVFX, float explosionRadius)
    {
        _explosionVFX = explosionVFX;
        _explosionRadius = explosionRadius;
    }

    public void ExplodeObjects(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Collider[] _colliders = Physics.OverlapSphere(hitInfo.point, _explosionRadius);

            foreach (Collider collider in _colliders)
            {
                IExplosible explosion = collider.GetComponent<IExplosible>();

                if (explosion != null)
                    explosion.Explode(hitInfo.point);
            }

            SpawnExplosionVFX(hitInfo.point);
        }
    }

    private void SpawnExplosionVFX(Vector3 spawnPoint)
    {
        Instantiate(_explosionVFX, spawnPoint, Quaternion.identity);
    }
}