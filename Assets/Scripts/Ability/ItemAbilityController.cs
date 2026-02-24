using UnityEngine;

public class ItemAbilityController : MonoBehaviour
{
    [SerializeField] private ItemAbilityPointMarker _itemPoint;    
    public AbilityBase _ability;                                    
    public GameObject _abilityObject;
    public bool EnableParent;
    public bool _isParent;

    private void Awake()
    {
        _itemPoint = GetComponentInChildren<ItemAbilityPointMarker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isParent == false)
        {
            _abilityObject = other.gameObject;
            
            if (other.TryGetComponent<ItemAbilityHealth>(out var itemAbilityHealth))
            {
                if (itemAbilityHealth != null && _isParent == false)
                {
                    _isParent = true;
                    _ability = itemAbilityHealth.GetAbility();                                                  
                }
            }

            if (other.TryGetComponent<ItemAbilitySpeed>(out var itemAbilitySpeed))
            {
                if (itemAbilitySpeed != null && _isParent == false)
                {
                    _isParent = true;
                    _ability = itemAbilitySpeed.GetAbility();                                     
                }
            }
            if (other.TryGetComponent<ItemAbilityGun>(out var itemAbilityGun))
            {
                if (itemAbilityGun != null && _isParent == false)
                {
                    _isParent = true;
                    _ability = itemAbilityGun.GetAbility();                                       
                }
            }
        }
    }

    private void Update()
    {
        if (_isParent)
            _abilityObject.transform.position = _itemPoint.transform.position;
    }
}