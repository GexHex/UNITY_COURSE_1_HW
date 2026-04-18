using System.Collections;
using UnityEngine;

public class MaterialDeathEffect : MonoBehaviour
{
    private float _elapsedTime;
    private float _timeLimit = 3;
    private SkinnedMeshRenderer[] _renderers;
    [SerializeField] Material _materialDeath;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveTimer());
    }

    private IEnumerator DissolveTimer()
    {
        _elapsedTime = 0;

        while (_elapsedTime < _timeLimit)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _timeLimit)
                _elapsedTime = _timeLimit;

            DissolveMaterial();
            yield return null;
        }
    }

    private void DissolveMaterial()
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.SetFloat("_Edge", _elapsedTime / _timeLimit);
            //_materialDeath.SetFloat("_Edge", _elapsedTime / _timeLimit);
        }
    }
}