using UnityEngine;

public class InputUser : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcherController;
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private Camera _camera;
    private MoveCameraRayController _moveFromCameraController;
    private ExplosionCameraRayController _explosionController;
    private CameraRayController _rayController;
    private float _explosionRadius = 2;       

    private void Awake()
    {
        _moveFromCameraController = new MoveCameraRayController(Camera.main, LayerMask.GetMask("ObjectMove"));
        _explosionController = new (_explosionVFX, _explosionRadius);
        _rayController = new CameraRayController(_camera);
    }

    private void Update()
    {
        Ray ray = _rayController.GetRay();

        if (Input.GetMouseButtonDown(0))
        {         
            _moveFromCameraController.OnGrab(ray);
        }

        if (Input.GetMouseButton(0))
        {
            _moveFromCameraController.Move(ray);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _moveFromCameraController.OnRelease();
        }
        //-------------------------- Explode -----------------------
        if (Input.GetMouseButtonDown(1))
        {
            _explosionController.ExplodeObjects(ray);
        }
        //-------------------------- Camera --------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cameraSwitcherController.SwitchCamera();
        }

        _moveFromCameraController.Update();
    }
}