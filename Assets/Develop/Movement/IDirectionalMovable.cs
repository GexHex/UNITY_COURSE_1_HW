using UnityEngine;

public interface IDirectionalMovable
{
    Vector3 Position { get; }

    Vector3 CurrentVelocity { get; set; }

    void SetMoveDirection(Vector3 inputDirection);

    void Update(float deltaTime);
}