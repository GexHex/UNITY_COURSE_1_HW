using UnityEngine;

public class ControllerDirectionalMoverCharacter : IDirectionalMovable
{
    private CharacterController _characterController;
    private float _movementSpeed;
    private Vector3 _currentDirection;

    public ControllerDirectionalMoverCharacter(CharacterController characterController, float movementSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
    }

    public Vector3 CurrentVelocity { get; set; }

    public Vector3 Position => _characterController.transform.position;

    public void SetMoveDirection(Vector3 inputDirection) => _currentDirection = inputDirection; 

    public void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _movementSpeed;
        _characterController.Move(CurrentVelocity * deltaTime);
    }
}