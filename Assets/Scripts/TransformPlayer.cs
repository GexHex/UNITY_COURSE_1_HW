using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    [SerializeField] private Player _palayer;
    [SerializeField] private DoubleJumpCheck _doubleJumpCheck;

    private Rigidbody _rigidbody;

    private string _horizontalAxis = "Horizontal";
    private string _verticallAxis = "Vertical";
    private KeyCode _jumpKey = KeyCode.Space;

    private float _rotateSpeed = 5;
    private float _jumpPower = 6;

    private float _xInput;
    private float _zInput;
    private bool _isJumpPressed;

    private bool _isGameOver = true;    

    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        _rigidbody = _palayer.GetComponent<Rigidbody>();
    }  

    private void Update()
    {
        if (_isGameOver)
        {
            _xInput = Input.GetAxisRaw(_horizontalAxis);
            _zInput = Input.GetAxisRaw(_verticallAxis);

            if (Input.GetKeyDown(_jumpKey) && _doubleJumpCheck.CanJump)
                _isJumpPressed = true;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.freezeRotation = true;
        }
    }

    public void GameOver()
    {
        _isGameOver = false;
    }

    private void FixedUpdate()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // »гнорируем наклон камеры по Y дл€ движени€
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // —оздаем вектор движени€ относительно камеры
        Vector3 direction = (cameraForward * _zInput + cameraRight * _xInput).normalized;

        // ѕримен€ем крут€щий момент в этом направлении
        if (direction.magnitude > 0.1f)
        {
            _rigidbody.AddTorque(new Vector3(direction.z, 0, -direction.x) * _rotateSpeed);
        }   
        
        if (_isJumpPressed)
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJumpPressed = false;
        }
    }   
}