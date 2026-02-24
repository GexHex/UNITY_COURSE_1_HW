using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerTransform _playerTransform;
    [SerializeField] private Animator _animator;
    private float _deadZone = 0.001f;
    private float _walkSpeed = 2;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_playerTransform.Direction.magnitude > _deadZone && _playerTransform.Speed <= _walkSpeed)
        {
            _animator.SetBool("IsWalk", true);
        }
        else
        {
            _animator.SetBool("IsWalk", false);
        }

        if (_playerTransform.Direction.magnitude > _deadZone && _playerTransform.Speed > _walkSpeed)
        {
            _animator.SetBool("IsRunOnce", true);
            _animator.SetBool("IsWalk", false);

            _animator.SetBool("IsRun", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }        
    }
}