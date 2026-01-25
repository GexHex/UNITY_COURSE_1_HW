using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _rotateSpeed;

    void Update()
    {
        _transform.Rotate(new Vector3(0, 1, 0) * -_rotateSpeed * Time.deltaTime);    
    }
}