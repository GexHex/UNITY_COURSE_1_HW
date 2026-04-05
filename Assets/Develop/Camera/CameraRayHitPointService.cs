using UnityEngine;

public class CameraRayHitPointService
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _flagObject;   
    private Ray _cameraRay;    

    public CameraRayHitPointService(Camera camera)
    {
        _camera = camera;      
    }

    public Vector3 RayHitPoint { get; private set; }

    public void Update()
    {
        _cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_cameraRay, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent<CameraClickObjectMarker>(out _))
            {
                RayHitPoint = hitInfo.point;
            }        
        }
    } 
}