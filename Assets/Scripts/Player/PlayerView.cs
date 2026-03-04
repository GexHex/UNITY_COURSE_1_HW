using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UserInput _input;
    private float _deadZone = 0.001f;
    private string _animatorIsRunning = "IsRunning";

    private void Update()
    {
        Vector3 input = _input.GetDirection();

        if (input != Vector3.zero)
            _animator.SetBool(_animatorIsRunning, true);
        else
            _animator.SetBool(_animatorIsRunning, false);

        if (input.magnitude <= _deadZone)
            return;
    }
}