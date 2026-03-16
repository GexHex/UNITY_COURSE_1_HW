using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private float _damageRadius = 2;
    private IMovable _movable;
    private Camera _camera;    
    private int _maskObjectMove;
    private bool _isDragging;
    private Plane _objectYLevelPlane;
    private Vector3 _offset;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _maskObjectMove = LayerMask.GetMask("ObjectMove");
        _camera = Camera.main;
    }

    public void PickObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _maskObjectMove))
        {
            if (hit.collider.TryGetComponent<IMovable>(out _movable))
            {
                _movable.SetRigidbodyProperties(false);

                _objectYLevelPlane = new Plane(Vector3.up, _movable.Position.position);

                if (_objectYLevelPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    _offset = hitPoint - _movable.Position.position;
                }

                _isDragging = true;
            }
        }
    }

    public void TransformObject()
    {
        if (_isDragging == false || _movable == null)
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_objectYLevelPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _targetPosition = hitPoint - _offset;
        }
    }

    public void DropObject()
    {
        if (_movable == null)
            return;

        _movable.SetRigidbodyProperties(true);
        _movable = null;
        _isDragging = false;
    }

    private void FixedUpdate()
    {
        if (_isDragging && _movable != null)        
            _movable.Move(_targetPosition);        
    }

    //-------------------------------- Explosion --------------------------------

    public void ExplosionObjects()
    {
        Ray _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_cameraRay, out RaycastHit hitInfo))
        {
            Collider[] _colliders = Physics.OverlapSphere(hitInfo.point, _damageRadius);

            foreach (Collider collider in _colliders)
            {
                IExplosible explosion = collider.GetComponent<IExplosible>();

                if (explosion != null)
                    explosion.Explosion(hitInfo.point);

                ExplosionVFX(hitInfo.point);
            }
        }
    }

    private void ExplosionVFX(Vector3 spawnPoint)
    {
        Instantiate(_explosionVFX, spawnPoint, Quaternion.identity);
    }
}