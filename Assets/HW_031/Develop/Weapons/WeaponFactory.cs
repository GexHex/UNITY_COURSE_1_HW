using UnityEngine;

public class WeaponFactory
{
    public Gun CreateGun(Gun gunPrefab, Transform gunPosition, Character instance)
    {
        Gun instanceGun = Object.Instantiate(gunPrefab, gunPosition.position, gunPosition.rotation, gunPosition);

        return instanceGun;
    }
}