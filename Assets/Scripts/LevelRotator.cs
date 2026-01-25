using UnityEngine;

public class LevelRotator : MonoBehaviour
{
    private float _horizontalRotate;
    private float _verticalRotate;
    private float _rotateSpeed = 0.2f;   

    private void Update()
    {
        _horizontalRotate = Input.GetAxisRaw("Horizontal");
        _verticalRotate = Input.GetAxisRaw("Vertical");

        Vector3 startAngles = transform.localEulerAngles; 

        transform.Rotate(_horizontalRotate * _rotateSpeed, 0, 0);
        transform.Rotate(0, 0, _verticalRotate * _rotateSpeed);                            
    }
}