using System;

public class ConditionSurviveTime : ICondition
{
    public event Action Completed;

    private float _currentTime;
    private float _timeToWin;
    private bool _isCompleted;

    public ConditionSurviveTime(float timeToWin)
    {
        _timeToWin = timeToWin;
    }

    public void Update(float deltaTime)
    {
        if (_isCompleted)
            return;

        _currentTime += deltaTime;

        if (_currentTime >= _timeToWin)
        {
            _isCompleted = true;
            Completed?.Invoke();
        }
    }
}