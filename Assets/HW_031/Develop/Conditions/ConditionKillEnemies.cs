using System;

public class ConditionKillEnemies : ICondition
{
    public event Action Completed;

    private readonly int _enemiesToKill;
    private int _currentKilledEnemies;
    private bool _isCompleted;

    public ConditionKillEnemies(ReactiveList<CharacterAgent> enemies, int enemiesToKill)
    {
        _enemiesToKill = enemiesToKill;

        enemies.Removed += OnEnemyRemoved;
    }

    private void OnEnemyRemoved(CharacterAgent enemy)
    {
        if (_isCompleted)
            return; 
                    
        _currentKilledEnemies++;

        if (_currentKilledEnemies >= _enemiesToKill)
        {
            _isCompleted = true; 
            Completed?.Invoke(); 
        }
    }

    public void Update(float deltaTime) 
    {

    }
}