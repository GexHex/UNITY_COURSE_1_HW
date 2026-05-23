using UnityEngine;

public class Dragon : BaseEnemy<ConfigDragon>
{
    private int _fireDamage;
    private float _flySpeed;

    public override void Setup(ConfigDragon config)
    {
        _fireDamage = config.FireDamage;
        _flySpeed = config.FlySpeed;

        Debug.Log($"{this.name} | FireDamage: {_fireDamage} FlySpeed: {_flySpeed}");
    }
}