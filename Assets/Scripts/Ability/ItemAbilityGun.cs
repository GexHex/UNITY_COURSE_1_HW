using UnityEngine;

public class ItemAbilityGun : MonoBehaviour
{                                               
    [SerializeField] private GameObject _deadEffect;
    private AbilityBase _abilityGun;

    private void Awake()
    {
        _abilityGun = new AbilityGun();
    }
  
    public AbilityBase GetAbility()
    {
        return _abilityGun;
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}