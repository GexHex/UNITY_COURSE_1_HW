using UnityEngine;

public class Health 
{
    public int Value { get; private set; }

    public Health(int health)
    {
        Value = health;
    }

    public void TakeDamage(int damage)
    {
        if (Value <= 0)
            return;

        Value -= damage;

        Debug.Log($"Здоровье: {Value}");
    }

    public void Heal(int value)
    {
        if (Value <= 0)
            return;

        Value += value;

        Debug.Log($"Здоровье: {Value}");
    }
}