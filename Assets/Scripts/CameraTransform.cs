using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private Transform _target;    
    [SerializeField] private float _rotateSpeed = 50;
    private Vector3 _offset;

    private void Awake()
    {       
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(_target.position, Vector3.up, _rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(_target.position, -Vector3.up, _rotateSpeed * Time.deltaTime);
        }

        _offset = transform.position - _target.position;
    } 
}