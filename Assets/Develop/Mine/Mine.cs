using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Mine : MonoBehaviour
{
    private const string Key1 = "_timeSin";
    private const string Key2 = "_alarmBlend";

    [SerializeField] private AudioClip _explosionClip;
    [SerializeField] private AudioMixerGroup _sfxMixer;
    [SerializeField] private MineView _mineView;
    [SerializeField] private MeshRenderer _meshRenderer;

    private float _mineDistanceDamage = 2f;
    private float _timeToExplosion = 2;
    private float _timeToDestroyMime = 0.2f;
    private int _damageValue = 10;    
    
    private float _time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var entity))
        {            
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        _meshRenderer.material.SetFloat(Key2, 20);

        while (_time <= _timeToExplosion)
        {
            _meshRenderer.material.SetFloat(Key1, _time);
            _time += Time.deltaTime;
            yield return null;
        }
        //------------------------------------------------------------------------------
        yield return null;

        Collider[] _colliders = Physics.OverlapSphere(transform.position, _mineDistanceDamage);

        foreach (Collider collider in _colliders)
        {
            IDamageable objectToDamage = collider.GetComponent<IDamageable>();

            if (objectToDamage != null)
                objectToDamage.TakeDamage(_damageValue);
        }
        
        PlayExplosionEffect();
        ShowDamageRadius();

        yield return new WaitForSeconds(_timeToDestroyMime);

        Destroy(gameObject);
    }

    private void PlayExplosionEffect()
    {
        _mineView.InstantiateEffect(transform.position);
        SoundPlayer.Play(_explosionClip, transform.position, _sfxMixer);
    }

    private void ShowDamageRadius()
    {
        _mineView.SetDamageScaleView(_mineDistanceDamage / 5);
    }
}