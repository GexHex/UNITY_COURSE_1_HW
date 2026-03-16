using UnityEngine;

public class ObjectExplosionabble : MonoBehaviour, IExplosible
{
    public void Explosion(Vector3 explosionVector)
    {
        Vector3 explosion = transform.position - explosionVector;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = explosion * 10;
    }
}