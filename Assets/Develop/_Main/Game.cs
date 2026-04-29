using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private InputUser _inputUser;
    private ControllerRigidbody2D _controllerRigidbody2D;    

    private void Start()
    {
        _controllerRigidbody2D = new ControllerRigidbody2D(_character, _character, _inputUser, _character);
        _controllerRigidbody2D.Enable();
    }

    private void FixedUpdate()
    {
        _controllerRigidbody2D.Update(Time.fixedDeltaTime);
    }
}