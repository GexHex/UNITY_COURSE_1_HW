using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }
}