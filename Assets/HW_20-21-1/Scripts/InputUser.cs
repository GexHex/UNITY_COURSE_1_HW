using UnityEngine;

public class InputUser : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcherController;
    [SerializeField] private InputController _objectControll;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _objectControll.PickObject();
        }

        if (Input.GetMouseButton(0))
        {
            _objectControll.TransformObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _objectControll.DropObject();
        }
        //-------------------------- Explosion -----------------------
        if (Input.GetMouseButtonDown(1))
        {
            _objectControll.ExplosionObjects();
        }
        //-------------------------- Camera --------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cameraSwitcherController.SwitchCamera();
        }
    }
}