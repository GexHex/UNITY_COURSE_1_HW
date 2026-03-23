using UnityEngine;

public class CameraRayController
{
    private Camera _camera;
    Ray ray;
    public CameraRayController(Camera camera)
    {
        _camera = camera;
    }

    public Ray GetRay()
    {       
        return ray = _camera.ScreenPointToRay(Input.mousePosition);
    }
}