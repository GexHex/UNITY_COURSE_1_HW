using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerTransform _playerTransform;
    [SerializeField] private Animator _animator;

    private float _deadZone = 0.001f;
    private float _walkSpeed = 2;

    private string _animatorIsWalk = "IsWalk";
    private string _animatorIsRun = "IsRun";
    private string _animatorIsOnce = "IsRunOnce";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_playerTransform.Direction.magnitude > _deadZone && _playerTransform.GetSpeed() <= _walkSpeed)
        {
            _animator.SetBool(_animatorIsWalk, true);
        }
        else
        {
            _animator.SetBool(_animatorIsWalk, false);
        }

        if (_playerTransform.Direction.magnitude > _deadZone && _playerTransform.GetSpeed() > _walkSpeed)
        {
            _animator.SetBool(_animatorIsOnce, true);
            _animator.SetBool(_animatorIsWalk, false);

            _animator.SetBool(_animatorIsRun, true);
        }
        else
        {
            _animator.SetBool(_animatorIsRun, false);
        }        
    }
}