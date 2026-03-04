using UnityEngine;

public class UserInput : MonoBehaviour
{
    private string _horizontalAxisName = "Horizontal";
    private string _verticalAxisName = "Vertical";
    private Vector3 _direction;

    private void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw(_horizontalAxisName), 0, Input.GetAxisRaw(_verticalAxisName));
    }

    public Vector3 GetDirection()
    {       
        return _direction;
    }
}