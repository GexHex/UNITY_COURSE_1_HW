using UnityEngine;

public class CameraRayHitPointService
{
    private Camera _camera;
    private Ray _cameraRay;
    private float _maxDistance = 1000f;
    private LayerMask _mask = LayerMask.GetMask("RaycastTarget", "RaycastIgnore");

    public CameraRayHitPointService(Camera camera)
    {
        _camera = camera;
    }

    public Vector3 RayHitPoint { get; private set; }
    public bool HasHitPoint { get; private set; }

    public void Update()
    {
        HasHitPoint = false;

        _cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_cameraRay, out RaycastHit hitInfo, _maxDistance, _mask))
        {
            if (hitInfo.collider.TryGetComponent<CameraClickObjectMarker>(out _))
            {
                RayHitPoint = hitInfo.point;
                HasHitPoint = true;
            }
        }
    }
}