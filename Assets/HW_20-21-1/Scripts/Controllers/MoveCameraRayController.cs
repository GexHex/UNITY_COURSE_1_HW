using UnityEngine;

public class MoveCameraRayController
{
    private IDraggable _movable;
    private int _maskObjectMove;
    private bool _isDragging;
    private Plane _objectYLevelPlane;
    private Vector3 _offset;
    private Vector3 _targetPosition;

    public MoveCameraRayController(Camera camera, int layerMask)
    {
        _maskObjectMove = layerMask;
    }

    public void OnGrab(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _maskObjectMove))
        {
            if (hit.collider.TryGetComponent<IDraggable>(out _movable))
            {
                _movable.OnGrab();

                _objectYLevelPlane = new Plane(Vector3.up, _movable.Position);

                if (_objectYLevelPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    _offset = hitPoint - _movable.Position;
                }

                _isDragging = true;
            }
        }
    }

    public void Move(Ray ray)
    {
        if (_isDragging == false || _movable == null)
            return;

        if (_objectYLevelPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _targetPosition = hitPoint - _offset;
        }
    }

    public void OnRelease()
    {
        if (_movable == null)
            return;

        _movable.OnRelease();
        _movable = null;
        _isDragging = false;
    }

    public void Update()
    {
        if (_isDragging && _movable != null)        
            _movable.Move(_targetPosition);        
    }
}