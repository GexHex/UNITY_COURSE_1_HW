using System.Collections;
using UnityEngine;

public class MineView : MonoBehaviour
{
    private const string InputTime = "_time";
    private const string AlarmColorBlend = "_alarmColorBlend";

    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] public GameObject _damageRadiusVisual;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private SoundController _soundController;

    private float _time;  

    public IEnumerator Explode(float timeToExplosion, float mineDistanceDamage)
    {
        _meshRenderer.material.SetFloat(AlarmColorBlend, 20);

        while (_time <= timeToExplosion)
        {
            _meshRenderer.material.SetFloat(InputTime, _time);
            _time += Time.deltaTime;
            yield return null;
        }

        yield return null;

        PlayExplosionEffect();
        ShowDamageRadius(mineDistanceDamage);
    }

    private void PlayExplosionEffect()
    {
        InstantiateEffect(transform.position);
        _soundController.PlayMineExplosionSFX(transform);
    }
   
    private void ShowDamageRadius(float mineDistanceDamage)
    {
        SetDamageScaleView(mineDistanceDamage / 5);
    }

    public void SetDamageScaleView(float scale)
    {
        _damageRadiusVisual.transform.localScale = new Vector3(scale, 0.1f, scale);
    }
    public void InstantiateEffect(Vector3 positionEffect)
    {
        Instantiate(_explosionVFX, positionEffect, Quaternion.identity);
    }
}