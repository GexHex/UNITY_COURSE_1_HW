using UnityEngine;

public class ObjectDragAndDropper : MonoBehaviour, IMovable
{
    private Rigidbody _rigidbody;
    private float _speed = 10;

    public Transform Position => transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 cameraRayHitPosition)
    {
        //transform.position = new Vector3(cameraRayHitPosition.x, transform.position.y, cameraRayHitPosition.z);
        //_rigidbody.Move(new Vector3(cameraRayHitPosition.x, transform.position.y, cameraRayHitPosition.z), Quaternion.identity);      
        //_rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position, new Vector3(cameraRayHitPosition.x, transform.position.y, cameraRayHitPosition.z), _speed * Time.deltaTime));
        _rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position, cameraRayHitPosition, _speed * Time.deltaTime));
    }

    public void SetRigidbodyProperties(bool set)
    {        
        //_rigidbody.isKinematic = !set;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = set;        
        _rigidbody.freezeRotation = !set;
    }
}