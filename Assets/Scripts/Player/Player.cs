using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private PlayerTransform _playerTransform;
    [SerializeField] private ItemAbilityController _itemAbilityController;

    private int _health = 100;
    private int _speed = 2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _itemAbilityController.Ability != null)
        {
            _itemAbilityController.Ability.UseAbility(this);
            DestroyAbility();
        }

        if (Input.GetKeyDown(KeyCode.F) && _itemAbilityController.AbilityObject == null)
        {
            PrintInfo("Нет абилки!");
        }
    }

    public void Shoot()
    {
        _gun.Shoot();
        PrintInfo($"Выстрел!");
    }

    public void AddHealth(int health)
    {
        _health += health;
        PrintInfo($"Здоровье увеличено: {_health}");
    }

    public void AddSpeed(int speed)
    {
        _speed += speed;
        PrintInfo($"Скорость увеличена: {_speed}");
    }

    private void DestroyAbility()
    {
        _itemAbilityController.IsParent = false;
        Destroy(_itemAbilityController.AbilityObject);
    }

    public float GetSpeed()
    {
        return _speed;
    }

    private void PrintInfo(string info)
    {
        Debug.Log(info);
    }
}