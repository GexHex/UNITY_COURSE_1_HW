using UnityEngine;

public class CharacterDirectionalMover : DirectionalMover
{
    private CharacterController _characterController;

    public CharacterDirectionalMover(CharacterController characterController, float movementSpeed) : base(movementSpeed)
    {
        _characterController = characterController;        
    }

    public override void Update(float deltaTime) => _characterController.Move(CurrentVelocity * deltaTime);    
}