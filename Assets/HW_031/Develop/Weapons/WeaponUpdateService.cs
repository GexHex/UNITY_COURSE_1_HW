using UnityEngine;

public class WeaponUpdateService
{
    private Gun _gun;

    public void SetGun(Gun gun)
    {
        _gun = gun;
    }

    public void Update()
    {
        if (_gun == null)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            _gun.Shoot();
        }
    }
}