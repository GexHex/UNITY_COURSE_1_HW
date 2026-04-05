using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private MineView _mineView;
    private float _time;
    private float _mineDistanceDamage = 2f;
    private float _mineDistanceTrigger = 1f;
    private float _timeToExplosion = 1;
    private int _damageValue = 10;
    private bool _isMineTriggered;
    private bool _hasExploded;

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasExploded && other.TryGetComponent<IDamageable>(out var entity))
        {
            _isMineTriggered = true;
        }
    }

    private void Update()
    {
        if (_isMineTriggered && _hasExploded == false)
        {
            MineTrigger();
        }
    }
    public void MineTrigger()
    {
        _time += Time.deltaTime;

        if (_time >= _timeToExplosion)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (_hasExploded) return;
        _hasExploded = true;

        Collider[] _colliders = Physics.OverlapSphere(transform.position, _mineDistanceDamage);

        foreach (Collider collider in _colliders)
        {
            IDamageable objectToDamage = collider.GetComponent<IDamageable>();

            if (objectToDamage != null)
                objectToDamage.TakeDamage(_damageValue);
        }

        PlayExplosionEffect();
        ShowDamageRadius();

        Destroy(gameObject, 0.2f);
    }

    public void PlayExplosionEffect()
    {
        _mineView.InstantiateEffect(transform.position);
    }

    public void ShowDamageRadius()
    {
        _mineView.SetDamageScaleView(_mineDistanceDamage / 5);
    }
}