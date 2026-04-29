using UnityEngine;

public class MoverRigidbody2D
{
    private ObstacleChecker _groundChecker;
    private ObstacleChecker _ceilChecker;
    private ObstacleChecker _wallCheckerRight;
    private ObstacleChecker _wallCheckerLeft;
    private float _yVelocityJump = 20f;
    private float _wallJumpHorizontalPower = 30;
    private float _wallJumpZerticalPower = 50;
    private Vector2 _velocity;
    private Rigidbody2D _rigidBody;    
    private Vector2 _damageVelocity;
    private float _gravity = 45f;   
    private bool _jumpPressed;

    public MoverRigidbody2D(Rigidbody2D rigidBody, ObstacleChecker groundChecker, ObstacleChecker ceilChecker, ObstacleChecker wallChecker, ObstacleChecker wallCheckerLeft)
    {
        _rigidBody = rigidBody;
        _groundChecker = groundChecker;
        _ceilChecker = ceilChecker;
        _wallCheckerRight = wallChecker;
        _wallCheckerLeft = wallCheckerLeft;
    }

    public void SetMoveDirection(Vector2 inputDirection) => _velocity.x = inputDirection.x;
    public void SetJumpPressed(bool isJumpPressed) => _jumpPressed = isJumpPressed;

    public void Update(float deltaTime)
    {
        if (!HandlerWallJump())
            HandlerJump();     
                             
        HandlerCeil();
        HandlerGravity(deltaTime);

        _damageVelocity = Vector2.Lerp(_damageVelocity, Vector2.zero, 10f * deltaTime);
        _rigidBody.velocity = _velocity + _damageVelocity;
    }
    public void HandlerGravity(float deltaTime)
    {
        if (_groundChecker.IsTouched())
        {
            if (_velocity.y < 0f)
                _velocity.y = 0f;

            return;
        }

        _velocity.y -= _gravity * deltaTime;
    }

    public void HandlerJump()
    {
        if (_jumpPressed && _groundChecker.IsTouched())
        {
            _velocity.y = _yVelocityJump;
            _jumpPressed = false;
        }
    }

    public void HandlerCeil()
    {
        if (_ceilChecker.IsTouched())
            _velocity.y = Mathf.Min(0, _velocity.y);
    }

    public bool HandlerWallJump()
    {
        if (_jumpPressed && !_groundChecker.IsTouched())
        {
            if (_wallCheckerRight.IsTouched())
            {
                _velocity.y = _wallJumpZerticalPower;
                _velocity.x = -(_wallJumpHorizontalPower);
            }

            if (_wallCheckerLeft.IsTouched())
            {
                _velocity.y = _wallJumpZerticalPower;
                _velocity.x = _wallJumpHorizontalPower;
            }                

            if (!_wallCheckerLeft.IsTouched() && !_wallCheckerRight.IsTouched())
                return false;

            _velocity.y = _yVelocityJump;

            _jumpPressed = false;

            return true;
        }

        return false;
    }

    public void GetDamage(Vector2 hitDirection)
    {
        _damageVelocity += hitDirection.normalized * 2f;
    }
}