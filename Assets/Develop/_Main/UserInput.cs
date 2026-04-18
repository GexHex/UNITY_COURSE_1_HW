using UnityEngine;

public class UserInput : MonoBehaviour
{
    public bool IsMouseDown()
    {
        return Input.GetMouseButtonDown(0);
    }
}