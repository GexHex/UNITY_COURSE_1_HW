using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    private Vector3 _cameraOffset;

    private void Awake()
    {
        _cameraOffset = transform.position;
    }

    private void Update()
    {
        transform.position = _playerPosition.position + _cameraOffset;
    }
}