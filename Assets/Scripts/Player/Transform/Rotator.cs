using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private float _rotationSpeed = 800;

    private void Update()
    {
        Vector3 direction = _userInput.GetDirection();
        ProcessRotateTo(direction);
    }
    private void ProcessRotateTo(Vector3 direction)
    {
        if (direction.magnitude > 0.001f)
        {
            Quaternion LookRotation = Quaternion.LookRotation(direction);
            float step = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookRotation, step);
        }
    }
}
