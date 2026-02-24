using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _deadVFX;
    private float _speed = 5;
    private float _timeToDeath = 2f;

    public void Awake()
    {
        Destroy(gameObject, _timeToDeath);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            PlayEffect();
        }
    }

    private void PlayEffect()
    {
        _deadVFX.transform.position = transform.position;
        Instantiate(_deadVFX);
    }
}