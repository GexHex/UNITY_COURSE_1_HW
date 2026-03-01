using UnityEngine;

public class ItemAbilityHealth : AbilityBase
{
    [SerializeField] private int Health = 10;
    [SerializeField] private GameObject _deadEffect;    

    public override void UseAbility(Player player)
    {          
        player.AddHealth(Health);    
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}