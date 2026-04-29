using UnityEngine;

public class ControllerRigidbody2D : Controller
{
    private IDirectionMovable _movable;
    private IDirectionRotatable _rotatable;
    private Character _character;
    private InputUser _inputUser;
    private bool _jumpPressed;
    private float _speed = 6;
    private Vector2 _velocity;
    private float _xInput;
    private float _horizontalVelocity;

    public ControllerRigidbody2D(IDirectionMovable movable, IDirectionRotatable rotatable, InputUser inputUser, Character character)
    {
        _inputUser = inputUser;
        _movable = movable;
        _rotatable = rotatable;
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _jumpPressed = _inputUser.GetJump();
        _xInput = _inputUser.GetXInput();

        _horizontalVelocity = _speed * _xInput;
        _velocity = new Vector2(_horizontalVelocity, 0);

        if (_character.Health <= 0)
        {
            _movable.SetMoveDirection(new Vector2(0, 0));
        }
        else        
        {
            _movable.SetMoveDirection(_velocity);
            _movable.SetJumpPressed(_jumpPressed);
            _rotatable.SetRotationDirection(_velocity);
        }
    }
}