using UnityEngine;

public class InputUser : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private float _xInput;
    private bool _jumpPressed;

    private void Update()
    {
        _xInput = Input.GetAxisRaw(HorizontalAxisName);

        if (Input.GetKeyDown(KeyCode.Space))
            _jumpPressed = true;
    }

    public float GetXInput() => _xInput;

    public bool GetJump()
    {
        bool value = _jumpPressed;
        _jumpPressed = false;

        return value;
    }
}