using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int VelocityXKey = Animator.StringToHash("VelocityX");
    private readonly int VelocityYKey = Animator.StringToHash("VelocityY");
    private readonly int IsGroundedKey = Animator.StringToHash("IsGrounded");
    private readonly int HitKey = Animator.StringToHash("Hit");

    [SerializeField] private Character _character;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        _animator.SetFloat(VelocityXKey, Mathf.Abs(_character.Velocity.x));
        _animator.SetFloat(VelocityYKey, _character.Velocity.y);
        _animator.SetBool(IsGroundedKey, _character.IsGrounded);

        if (_character.IsHit)
        {
            _animator.SetTrigger(HitKey);
            _character.ResetHit();
        }
    }
}