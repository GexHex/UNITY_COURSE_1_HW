using UnityEngine;

public interface IDirectionalMovable
{
    Vector3 CurrentVelocity { get; }
    
    Vector3 Position { get; }

    void SetMoveDirection(Vector3 inputDirection);
}