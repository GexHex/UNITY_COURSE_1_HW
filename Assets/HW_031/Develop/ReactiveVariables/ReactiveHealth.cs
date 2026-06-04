using System;
using UnityEngine;

public class ReactiveHealth 
{
    public event Action HealthChanged;
    public event Action Died;

    public int Value { get; private set; }

    public ReactiveHealth(int health)
    {
        Value = health;
    }

    public void TakeDamage(int value, string objectName)
    {
        if (Value <= 0 || value <= 0)         
            return;

        Value -= value;

        if (Value < 0)
            Value = 0;

        HealthChanged?.Invoke();

        Debug.Log($"Здоровье объекта изменилось: {objectName}: {Value}");

        if(Value == 0)
            Died?.Invoke();
    }

    public void Heal(int value, string objectName)
    {
        if (Value <= 0 || value <= 0)
            return;

        Value += value;

        HealthChanged?.Invoke();

        Debug.Log($"Здоровье объекта увеличилось: {objectName}: {Value}");
    }
}