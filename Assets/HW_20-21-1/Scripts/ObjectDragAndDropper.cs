using UnityEngine;

public class ObjectDragAndDropper : MonoBehaviour, IDraggable
{
    private Rigidbody _rigidbody;
    private float _speed = 10;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 cameraRayHitPosition)
    {
        _rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position, cameraRayHitPosition, _speed * Time.deltaTime));
    }   

    public void OnGrab()
    {  
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    public void OnRelease()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = false;
    }
}