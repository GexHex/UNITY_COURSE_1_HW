using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Rigidbody2D _rigidbody;
    private bool _isFalling;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isFalling) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _isFalling = true;
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(_delay);

        _rigidbody.isKinematic = false;
        _rigidbody.gravityScale = 10;
    }
}