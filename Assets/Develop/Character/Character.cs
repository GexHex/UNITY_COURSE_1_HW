using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public int Health { get; private set; } = 150;
    public IDirectionalMovable Movable => _mover;
    public IDirectionalRotatable Rotatable => _rotator;
    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 Position => transform.position;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private ControllerDirectionalMoverCharacter _mover;
    private ControllerDirectionalRotator _rotator;   

    private ICompositeController _currentController;
    private float _timeCooldown;
    private float _timeAnimationCooldown = 1f;
    private int _currentCharacterHealth;

    private void Awake()
    {
        _mover = new ControllerDirectionalMoverCharacter(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new ControllerDirectionalRotator(transform, _rotationSpeed);
    }

    public void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetController(ICompositeController controller)
    {
        _currentController = controller;
    } 

    public void MoveControll(float deltaTime)
    {
        if (Health > 0)
        {
            _currentController.Enable();
        }

        if (_currentCharacterHealth != Health)
        {
            _timeCooldown += deltaTime;

            if (_timeCooldown < _timeAnimationCooldown)
            {
                _currentController.Disable();
                _currentController.StopMove();
            }
            else
            {
                _timeCooldown = 0;

                _currentController.Enable();

                _currentCharacterHealth = Health;
            }
        }

        if (_currentCharacterHealth <= 0)
        {
            _currentController.Disable();
            _currentController.StopMove();
        }
    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;      

        Debug.Log($"Здоровье: {Health}");
    }
}