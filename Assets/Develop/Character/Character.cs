using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public int Health { get; private set; } = 150;
    public DirectionalMover Movable => _mover;
    public DirectionalRotator Rotatable => _rotator;
    public Vector3 CurrentVelocity => _mover.CurrentVelocity;


    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private void Awake()
    {
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    public void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;      

        Debug.Log($"Здоровье: {Health}");
    }
}