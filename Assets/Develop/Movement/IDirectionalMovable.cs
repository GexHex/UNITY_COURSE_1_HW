using UnityEngine;

public interface IDirectionalMovable : ITransformPositon
{
    Vector3 CurrentVelocity { get; }

    void SetMoveDirection(Vector3 inputDirection);
}