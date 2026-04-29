using UnityEngine;

public interface IDirectionMovable
{
    void SetMoveDirection(Vector2 inputDirection);
    void SetJumpPressed(bool isJumpPressed);
}