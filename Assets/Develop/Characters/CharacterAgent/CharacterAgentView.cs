using UnityEngine;

public class CharacterAgentView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAgent _characterAgent;

    private readonly int _inJumpProcessKey = Animator.StringToHash("InJumpProcess");
    private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
    private readonly int _isDeadKey = Animator.StringToHash("IsDead");
    private readonly int _isHitKey = Animator.StringToHash("Hit");

    private int _tempHealthValue;
    private int _healthToChangeAnimation = 100;

    private bool _isDead;

    private void Start()
    {
        _tempHealthValue = _characterAgent.Health;
    }

    private void Update()
    {
        int currentHealth = _characterAgent.Health;

        if (currentHealth <= 0)
        {
            _animator.SetTrigger(_isDeadKey);
            return;
        }

        _animator.SetBool(_inJumpProcessKey, _characterAgent.InJumpProcess);

        UpdateRunning();
        UpdateHealth();
        UpdateLowHealthLayer();
    }

    private void UpdateRunning()
    {
        if (_characterAgent.CurrentVelocity.sqrMagnitude > 0.0025f)
        {
            StartRunning();
        }
        else
        {
            StopRunning();
        }
    }

    private void UpdateHealth()
    {
        int currentHealth = _characterAgent.Health;

        if (currentHealth < _tempHealthValue)
        {
            _animator.SetTrigger(_isHitKey);
            _tempHealthValue = currentHealth;
        }
    }

    private void UpdateLowHealthLayer()
    {
        int currentHealth = _characterAgent.Health;

        if (currentHealth < _healthToChangeAnimation)
        {
            _animator.SetLayerWeight(1, 1);
        }
        else
        {
            _animator.SetLayerWeight(1, 0);
        }
    }

    private void StopRunning() => _animator.SetBool(_isRunningKey, false);
    private void StartRunning() => _animator.SetBool(_isRunningKey, true);
}