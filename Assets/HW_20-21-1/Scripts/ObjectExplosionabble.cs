using UnityEngine;

public class ObjectExplosionabble : MonoBehaviour, IExplosible
{
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explode(Vector3 explosionVector)
    {
        Vector3 explosion = transform.position - explosionVector;
        _rigidbody.velocity = explosion * 10;
    }
}