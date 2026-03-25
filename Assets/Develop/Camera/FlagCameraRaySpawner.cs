using UnityEngine;

public class FlagCameraRaySpawner
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _flagObject;
    private GameObject _flag;
    private Ray _cameraRay;

    public FlagCameraRaySpawner(Camera camera, GameObject flagObject)
    {
        _camera = camera;
        _flagObject = flagObject;
    }

    public Transform RayPosition { get; private set; }

    public void Update()
    {        
        SpawnFlag(_camera);       
    }

    public void SpawnFlag(Camera camera)
    {
        _cameraRay = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_cameraRay, out RaycastHit hitInfo))
        {
            SetFlag(hitInfo);
        }
    }

    private void SetFlag(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent<CameraClickObjectMarker>(out _))
        {
            GameObject.Destroy(_flag);
            _flag = GameObject.Instantiate(_flagObject, hitInfo.point, Quaternion.identity);

            RayPosition = _flag.transform;
        }
    }
}