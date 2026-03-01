using UnityEngine;

public class ItemAbilityController : MonoBehaviour
{
    public AbilityBase Ability { get; private set; }                                    
    public GameObject AbilityObject { get; set; }   
    public bool IsParent { get; set; }

    [SerializeField] private ItemAbilityPointMarker _itemPoint;

    private void Awake()
    {
        _itemPoint = GetComponentInChildren<ItemAbilityPointMarker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsParent == false)
        {
            AbilityObject = other.gameObject;
            
            if (other.TryGetComponent<AbilityBase>(out var ability))
            {
                if (ability != null && IsParent == false)
                {
                    IsParent = true;
                    Ability = ability;                                                  
                }
            }          
        }
    }

    private void Update()
    {
        if (IsParent)
            AbilityObject.transform.position = _itemPoint.transform.position;
    }
}