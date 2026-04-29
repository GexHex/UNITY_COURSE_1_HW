using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;

            damageable.TakeDamage(_damage, direction);
        }
    }
}