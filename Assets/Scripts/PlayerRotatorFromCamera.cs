using UnityEngine;

public class PlayerRotatorFromCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float _transform;   

    private void Update()
    {    
        transform.localRotation = _camera.transform.rotation;
    }
}