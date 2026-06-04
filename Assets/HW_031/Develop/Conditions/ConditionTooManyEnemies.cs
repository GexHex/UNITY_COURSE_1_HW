using System;

public class ConditionTooManyEnemies : ICondition
{
    public event Action Completed;

    private readonly int _maxEnemies;
    private int _currentEnemies;
    private bool _isCompleted;

    public ConditionTooManyEnemies(ReactiveList<CharacterAgent> enemies, int maxEnemies)
    {
        _maxEnemies = maxEnemies;

        enemies.Added += OnEnemyAdded;
        enemies.Removed += OnEnemyRemoved;
    }

    private void OnEnemyAdded(CharacterAgent enemy)
    {
        if (_isCompleted)
            return;

        _currentEnemies++;

        if (_currentEnemies >= _maxEnemies)
        {
            _isCompleted = true;
            Completed?.Invoke();
        }
    }

    private void OnEnemyRemoved(CharacterAgent enemy)
    {
        _currentEnemies--;
    }

    public void Update(float deltaTime)
    {

    }
}