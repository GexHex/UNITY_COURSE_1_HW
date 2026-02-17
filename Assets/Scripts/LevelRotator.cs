using UnityEngine;

public class LevelRotator : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _rotateSpeed = 50.0f;
    Rigidbody levelRigidbody;

    private void Awake()
    {
        levelRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");     
    }

    private void FixedUpdate()
    {
        Vector3 rotationInput = new Vector3(_horizontalInput, 0f, _verticalInput);  

        if (rotationInput.sqrMagnitude < 0.0001f)
            return;

        Quaternion deltaRotation = Quaternion.Euler(rotationInput * _rotateSpeed * Time.fixedDeltaTime);

        levelRigidbody.MoveRotation(levelRigidbody.rotation * deltaRotation);
    }
}