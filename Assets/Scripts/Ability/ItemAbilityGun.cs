using UnityEngine;

public class ItemAbilityGun : AbilityBase
{
    [SerializeField] private GameObject _deadEffect;

    public override void UseAbility(Player player)
    {       
        player.Shoot();
    }

    private void OnDestroy()
    {
        if (_deadEffect != null)
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }
}