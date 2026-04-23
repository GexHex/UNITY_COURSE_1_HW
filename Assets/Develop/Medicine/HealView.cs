using UnityEngine;

public class HealView : MonoBehaviour
{   
    [SerializeField] private GameObject _healVFX;
    private SoundController _soundController;

    public void Initialize(SoundController soundMainMixer)
    {
        _soundController = soundMainMixer;
    }

    public void InstantiateEffect(Vector3 positionEffect)
    {        
        Instantiate(_healVFX, positionEffect, Quaternion.identity);        
    }

    public void PlayHealEffect()
    {
        InstantiateEffect(transform.position);
        _soundController.PlayHealSFX(transform);
    }
}