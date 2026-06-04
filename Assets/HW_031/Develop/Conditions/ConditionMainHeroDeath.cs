using System;

public class ConditionMainHeroDeath : ICondition
{
    public event Action Completed;

    private Character _mainHero;
    private int _minHealth;
    private bool _isCompleted;

    public ConditionMainHeroDeath(Character mainHero, int minHealth)
    {
        _mainHero = mainHero;
        _minHealth = minHealth;
    }

    public void Update(float deltaTime)
    {
        if (_isCompleted)
            return;

        if (_mainHero.Health <= _minHealth)
        {
            _isCompleted = true;
            Completed?.Invoke();
        }
    }
}