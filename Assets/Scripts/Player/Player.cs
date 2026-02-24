using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ItemAbilityController _itemAbilityController;
    [SerializeField] private PlayerTransform _playerTransform;

    private int _health = 100;
    private int _speed = 5;   
    private AbilityBase _currentAbility;    

    private void Update()
    {
        if (_itemAbilityController._ability != null)   
        {
            _currentAbility = _itemAbilityController._ability;
            _itemAbilityController._ability = null;   
        }

        else if (Input.GetKeyDown(KeyCode.F) && _currentAbility is AbilityHealth)       
        {          
            int result = _currentAbility.UseAbility();                            
            _health += result;

            Other(_health);
            Debug.Log($"Здоровье: {_health}");
        }

        else if(Input.GetKeyDown(KeyCode.F) && _currentAbility is AbilitySpeed)       
        {
            int result = _currentAbility.UseAbility();
            _speed = Convert.ToInt32(_playerTransform.Speed += result);

            Other(_speed);
            Debug.Log($"Скорость: {_speed}");
        }

        else if(Input.GetKeyDown(KeyCode.F) && _currentAbility is AbilityGun)
        {
            int result = _currentAbility.UseAbility();
            _gun.CanShoot = result;

            Other(_gun.CanShoot);
            Debug.Log("Выстрел!");
        }

        else if (Input.GetKeyDown(KeyCode.F) && _currentAbility is null)
        {
            Debug.Log("Нет абилки!");
        }
    }

    private void Other(int result)
    {
        _itemAbilityController._isParent = false;       
        _currentAbility = null;
        Destroy(_itemAbilityController._abilityObject); 
    }
}