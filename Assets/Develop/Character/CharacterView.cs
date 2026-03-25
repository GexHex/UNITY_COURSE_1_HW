using UnityEngine;

public class CharacterView : MonoBehaviour
{   
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;
    private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
    private readonly int _isDeadKey = Animator.StringToHash("IsDead");
    private readonly int _isHitKey = Animator.StringToHash("Hit");
    private float _tempCharacterHealth;
    private int _healthToChangeAnimation = 50;

    private void Awake()
    {
        _tempCharacterHealth = _character.Health;
    }

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > 0.01f)
        {
            StartRunning();
        }
        else
        {
            StopRunning();
        }

        if (_tempCharacterHealth != _character.Health)
        {
            if (_character.Health <= 0)
            {              
                _animator.SetTrigger(_isDeadKey);
            }
            else
            {
                _animator.SetTrigger(_isHitKey);
                _tempCharacterHealth = _character.Health;
            }
        }

        if (_character.Health < _healthToChangeAnimation)
        {
            _animator.SetLayerWeight(1, 1);            
        }
    }

    private void StartRunning() => _animator.SetBool(_isRunningKey, true);
    private void StopRunning() => _animator.SetBool(_isRunningKey, false);      
}