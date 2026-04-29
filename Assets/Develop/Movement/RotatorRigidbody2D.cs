using UnityEngine;

public class RotatorRigidbody2D
{
    private Vector2 _velocity;
    private Transform _transform;

    public RotatorRigidbody2D(Transform transform)
    {
        _transform = transform;
    }

    public void Update(float deltaTime)
    {
        _transform.rotation = GetRotationFrom(_velocity);
    }

    public void SetRotationDirection(Vector2 inputDirection)
    {
        _velocity = inputDirection;
    }

    private Quaternion GetRotationFrom(Vector2 velocity)
    {
        if (_velocity.x > 0)
            return TurnRight;

        if (_velocity.x < 0)
            return TurnLeft;

        return _transform.rotation;
    }

    private Quaternion TurnRight => Quaternion.identity;
    private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);
}