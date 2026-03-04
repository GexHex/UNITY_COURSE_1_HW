using UnityEngine;

public class Mover : MonoBehaviour
{    
    [SerializeField] CharacterController _characterController;
    [SerializeField] UserInput _userInput;
    [SerializeField] float _speed;

    private void Update()
    {
        Vector3 input = _userInput.GetDirection();
        Vector3 normalizeInput = input.normalized;

        ProcessMoveTo(normalizeInput);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        _characterController.Move(direction * _speed * Time.deltaTime);
    }
}