using System.Collections;
using UnityEngine;

public class CharacterMaterialView : MonoBehaviour
{
    private const string InputTime = "_time";
    private const string AlarmColorBlend = "_alarmColorBlend";    

    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private float _cooldownAlarm = 3.14f;
    private float _time;
    private ReactiveHealth _health;  

    public void Initialize(ReactiveHealth health)
    {
        _health = health;
        _health.HealthChanged += HealthChanged;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= HealthChanged;
    }

    private void HealthChanged()
    {
        StartCoroutine(GetDamage(_cooldownAlarm));
    }

    public IEnumerator GetDamage(float CooldownTime)
    {
        _meshRenderer.material.SetFloat(AlarmColorBlend, 0);

        while (_time <= CooldownTime)
        {
            _meshRenderer.material.SetFloat(InputTime, _time);
            _time += Time.deltaTime;
            yield return null;
        }

        _time = 0;

        yield return null;
    }
}