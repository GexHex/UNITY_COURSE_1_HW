using UnityEngine;

public class RotatorService
{
    private Transform _transform;
    private float _rotationSpeed;
    private Vector3 _currentDirection;

    public RotatorService(Transform transform, float rotarionSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotarionSpeed;
    }

    public Quaternion CurrentRotation => _transform.rotation; 
    
    public void SetRotationDirection(Vector3 direction) => _currentDirection = direction;
 
    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);
        float step = _rotationSpeed * deltaTime;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }
}