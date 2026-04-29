using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _distanseToCheck;

    public bool IsTouched()
    {
        return Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, _direction, _distanseToCheck, _mask).collider != null;
    }
}