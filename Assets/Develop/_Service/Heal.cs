using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Heal : MonoBehaviour
{
    [SerializeField] private HealView _healView;

    [SerializeField] private AudioClip _explosionClip;
    [SerializeField] private AudioMixerGroup _sfxMixer;

    private float _mineDistanceHeal = 2f;
    private float _timeToExplosion = 0.2f;
    private int _healValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHealable>(out var entity))
        {
            StartCoroutine(Recover());
        }
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(_timeToExplosion);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _mineDistanceHeal);

        foreach (Collider collider in colliders)
        {
            IHealable objectToHeal = collider.GetComponent<IHealable>();

            if (objectToHeal != null)
                objectToHeal.Heal(_healValue);
        }

        PlayHealEffect();
        Destroy(gameObject);
    }

    private void PlayHealEffect()
    {
        _healView.InstantiateEffect(transform.position);
        SoundPlayer.Play(_explosionClip, transform.position, _sfxMixer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _mineDistanceHeal);
    }
}