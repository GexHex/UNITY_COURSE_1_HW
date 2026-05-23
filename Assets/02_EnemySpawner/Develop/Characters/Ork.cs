using UnityEngine;

public class Ork : BaseEnemy<ConfigOrk>
{
    private int _damage;
    private int _rage;

    public override void Setup(ConfigOrk config)
    {
        _damage = config.Damage;
        _rage = config.Rage;

        Debug.Log($"{this.name} | Damage: {_damage} Rage: {_rage}");
    }
}