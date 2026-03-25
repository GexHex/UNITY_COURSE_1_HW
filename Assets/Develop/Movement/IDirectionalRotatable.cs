using UnityEngine;

public interface IDirectionalRotatable : ITransformPositon
{
    Quaternion CurrentRotation {  get; }
    void SetRotationDirection(Vector3 inputDirection);
}
