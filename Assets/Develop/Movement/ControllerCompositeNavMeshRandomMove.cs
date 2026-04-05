using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerCompositeNavMeshRandomMove : ICompositeController
{
    public List<Vector3> _checkPoints { get; private set; }

    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;

    private Queue<Vector3> _checkPointsQueue = new Queue<Vector3>();
    private Vector3 _currentEnemyTarget;   
    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private float _minDistanceToTarget = 0.3f;    
    private float _radius = 5;
    private int _checkpointsCount = 5;
    private bool _isEnableUpdateLogic;

    public ControllerCompositeNavMeshRandomMove(IDirectionalMovable movable, IDirectionalRotatable rotatable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _rotatable = rotatable;
        _queryFilter = queryFilter;

        GetRandomCheckpoints();
    }

    public void Disable() => _isEnableUpdateLogic = false;

    public void Enable() => _isEnableUpdateLogic = true;

    public void Update(float deltaTime)
    {
        if (_isEnableUpdateLogic)
            UpdateLogic(deltaTime);
    }

    public void StopMove()
    {
        _movable.CurrentVelocity = new Vector3(0, 0, 0);
        _movable.SetMoveDirection(Vector3.zero);
    }

    public void GetRandomCheckpoints()
    {
        _checkPointsQueue.Clear();
        _checkPoints = NavMeshUtils.GetRandomPoints(_movable.Position, _radius, _checkpointsCount);        
        QueueGetTargets();
    }

    public void UpdateLogic(float deltaTime)
    {
        Vector3 direction = _currentEnemyTarget - _movable.Position;

        if (direction.magnitude <= _minDistanceToTarget)
            SwitchTarget();

        ProcessMoveTo();
    }

    private void ProcessMoveTo()
    {
        if (NavMeshUtils.TryGetPath(_movable.Position, _currentEnemyTarget, _queryFilter, _pathToTarget))
        {
            if (_pathToTarget.corners.Length >= 2)
            {
                Vector3 direction = _pathToTarget.corners[1] - _pathToTarget.corners[0];

                _movable.SetMoveDirection(direction.normalized);
                _rotatable.SetRotationDirection(direction);
            }
        }
    }

    private void SwitchTarget()
    {
        _currentEnemyTarget = _checkPointsQueue.Dequeue();
        _checkPointsQueue.Enqueue(_currentEnemyTarget);
    }

    private void QueueGetTargets()
    {
        foreach (Vector3 targets in _checkPoints)
            _checkPointsQueue.Enqueue(targets);

        _currentEnemyTarget = _checkPointsQueue.Peek();
    }
}