using UnityEngine;

public class Character : MonoBehaviour, IDirectionMovable, IDirectionRotatable, IDamageable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ObstacleChecker _groundChecker;
    [SerializeField] private ObstacleChecker _ceilChecker;
    [SerializeField] private ObstacleChecker _wallCheckerRight;
    [SerializeField] private ObstacleChecker _wallCheckerLeft;

    private MoverRigidbody2D _mover;
    private RotatorRigidbody2D _rotator;
    private int _heath = 100;
    private bool _hit;

    public int Health => _heath;
    public bool IsGrounded => _groundChecker.IsTouched();
    public Vector2 Velocity => _rigidbody.velocity;
    public bool IsHit => _hit;

    private void Awake()
    {
        _mover = new MoverRigidbody2D(_rigidbody, _groundChecker, _ceilChecker, _wallCheckerRight, _wallCheckerLeft);
        _rotator = new RotatorRigidbody2D(transform);
    }

    public void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        _heath -= damage;
        _hit = true;
        GetDamage(hitDirection);

        Debug.Log($"Здоровье: {_heath}");
    }

    public void ResetHit()
    {
        _hit = false;
    }

    public void SetMoveDirection(Vector2 inputDirection) => _mover.SetMoveDirection(inputDirection);
    public void SetJumpPressed(bool isJumpPressed) => _mover.SetJumpPressed(isJumpPressed);
    public void GetDamage(Vector2 hitDirection) => _mover.GetDamage(hitDirection);
    public void SetRotationDirection(Vector2 inputDirection) => _rotator.SetRotationDirection(inputDirection);
}