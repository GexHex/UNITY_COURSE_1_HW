using UnityEngine;

public class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable, IDamageable //ICanShoot
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _gunPosition;
    [SerializeField] private CharacterMaterialView _characterMaterialView;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private ReactiveHealth _health;

    public void Initialize(DirectionalMover mover, DirectionalRotator rotator, int health)
    {
        _mover = mover;
        _rotator = rotator;

        _health = new ReactiveHealth(health);

        _characterMaterialView.Initialize(_health);

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();
    }

    public int Health => _health.Value;

    public Transform GunPosition => _gunPosition;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public Transform CameraTarget => _cameraTarget;

    private void Update()
    {      
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(int damage, string damageObject)
    {
        _health.TakeDamage(damage, this.name);
    }
}