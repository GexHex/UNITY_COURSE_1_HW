using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InfoDisplay
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void LateUpdate()
        {
            if (_camera == null)
                _camera = Camera.main;

            if (_camera == null)
                return;

            transform.rotation = _camera.transform.rotation;
        }
    }
}
