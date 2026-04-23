using System.Collections;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private HealView _healView;
    private bool _isHealProcess;
    private float _mineDistanceHeal = 2f;
    private float _timeToExplosion = 0.2f;
    private int _healValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (_isHealProcess)
            return;

        if (other.TryGetComponent<IHealable>(out var entity))
        {
            _isHealProcess = true;
            StartCoroutine(Recover());
        }
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(_timeToExplosion);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _mineDistanceHeal);

        foreach (Collider collider in colliders)
        {
            IHealable objectToHeal = collider.GetComponent<IHealable>();

            if (objectToHeal != null)
                objectToHeal.Heal(_healValue);
        }

        _healView.PlayHealEffect();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _mineDistanceHeal / 5);
    }
}