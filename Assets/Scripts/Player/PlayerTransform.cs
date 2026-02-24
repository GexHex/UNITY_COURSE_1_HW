using UnityEngine;

public class PlayerTransform : MonoBehaviour
{    
    public Vector3 Direction { get; private set; }

    [SerializeField] public float Speed = 2;
    [SerializeField] private float _rotationSpeed = 300;
    private CharacterController _characterController;    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float _xInput = Input.GetAxisRaw("Horizontal");
        float _zInput = Input.GetAxisRaw("Vertical");

        Direction = new Vector3(_xInput, 0, _zInput);
        Direction = Direction.normalized; 
       
        Move();
        Rotate();
    }

    private void Move()
    {        
        _characterController.Move(Direction * Speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (Direction != Vector3.zero)
        {
            Quaternion LookRotation = Quaternion.LookRotation(Direction);
            float step = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookRotation, step);
        }
    }
}