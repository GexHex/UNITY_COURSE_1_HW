using UnityEngine;

public class ItemAbilitySpeed : AbilityBase
{
    [SerializeField] private int Speed = 2;
    [SerializeField] private GameObject _deadEffect;    

    public override void UseAbility(Player player)
    {    
        player.AddSpeed(Speed);
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}