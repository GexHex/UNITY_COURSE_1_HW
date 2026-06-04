using System;

public interface ICondition
{
    event Action Completed;

    void Update(float deltaTime);
}