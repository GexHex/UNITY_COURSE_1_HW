using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig _bulletConfig;
    [SerializeField] private DamageTrigger _damageTrigger;

    private GameObject _deadVFX;
    private float _speed;
    private float _timeToDeath;
    private int _damage;

    private void Awake()
    {
        _deadVFX = _bulletConfig.DeadVFX;
        _speed = _bulletConfig.Speed;
        _timeToDeath = _bulletConfig.TimeToExplosion;
        _damage = _bulletConfig.Damage;

        _damageTrigger.TriggerEntered += OnTriggerEntered;
    }

    private void Update()
    {
        _timeToDeath -= Time.deltaTime;

        if (_timeToDeath < 0 && gameObject != null)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {   
        DestroyBullet();
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.CompareTag("EnemyDamageArea"))
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage, other.name);

                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        _damageTrigger.TriggerEntered -= OnTriggerEntered;

        if (Application.isPlaying)
        {
            PlayEffect();
        }
    }

    private void PlayEffect()
    {
        _deadVFX.transform.position = transform.position;

        Instantiate(_deadVFX);
    }
}