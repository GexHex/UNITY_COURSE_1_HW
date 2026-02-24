using UnityEngine;

public class ItemAbilitySpeed : MonoBehaviour
{                                               
    [SerializeField] private GameObject _deadEffect;
    private AbilityBase _abilitySpeed;

    private void Awake()
    {
        _abilitySpeed = new AbilitySpeed();
    }

    public AbilityBase GetAbility()
    {
        return _abilitySpeed;
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}