using UnityEngine;

public class ItemAbilityHealth : MonoBehaviour
{                                               
    [SerializeField] private GameObject _deadEffect;
    private AbilityBase _abilityHealth;

    private void Awake()
    {
        _abilityHealth = new AbilityHealth();
    }
 
    public AbilityBase GetAbility()
    {
        return _abilityHealth;
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}