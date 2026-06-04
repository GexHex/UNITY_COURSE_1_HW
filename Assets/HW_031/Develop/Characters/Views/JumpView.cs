using UnityEngine;

public class JumpView : MonoBehaviour, IInitializable
{
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");

    [SerializeField] private Animator _animator;

    private IJumper _jumper;

    private bool _isInit;

    public void Initialize()
    {
        _jumper = GetComponentInParent<IJumper>();

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        _animator.SetBool(InJumpProcessKey, _jumper.InJumpProcess);
    }
}