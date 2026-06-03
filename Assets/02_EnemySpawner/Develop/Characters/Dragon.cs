using UnityEngine;

public class Dragon : BaseEnemy
{
    private int _fireDamage;
    private float _flySpeed;

    public void Setup(ConfigDragon config)
    {
        _fireDamage = config.FireDamage;
        _flySpeed = config.FlySpeed;

        Debug.Log($"{this.name} | FireDamage: {_fireDamage} FlySpeed: {_flySpeed}");
    }
}