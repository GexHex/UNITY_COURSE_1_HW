using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private MineView _mineView;  
    private bool _isExplodeProcess;
    private float _mineDistanceDamage = 2f;
    private float _timeToDestroyMine = 0.2f;
    private int _damageValue = 10;
    private float _timeToExplosion = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (_isExplodeProcess)
            return;

        if (other.TryGetComponent<IDamageable>(out var entity))
        {
            _isExplodeProcess = true;
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {       
        StartCoroutine(_mineView.Explode(_timeToExplosion, _mineDistanceDamage));

        yield return new WaitForSeconds(_timeToExplosion);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _mineDistanceDamage);

        foreach (Collider collider in colliders)
        {
            IDamageable objectToDamage = collider.GetComponent<IDamageable>();

            if (objectToDamage != null)
                objectToDamage.TakeDamage(_damageValue);
        }

        yield return new WaitForSeconds(_timeToDestroyMine);

        Destroy(gameObject);
    }
}