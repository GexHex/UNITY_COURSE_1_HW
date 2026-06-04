using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerRandomPoints : Controller
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

    public ControllerRandomPoints(IDirectionalMovable movable, IDirectionalRotatable rotatable)
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _movable = movable;
        _rotatable = rotatable;
        _queryFilter = queryFilter;

        GetRandomCheckpoints();
    }

    public override void Enable()
    {
        base.Enable();
        GetRandomCheckpoints();
    }

    //public override void StopMove()
    //{
    //    _movable.SetMoveDirection(Vector3.zero);
    //}

    public void GetRandomCheckpoints()
    {
        _checkPointsQueue.Clear();
        _checkPoints = NavMeshUtils.GetRandomPoints(_movable.Position, _radius, _checkpointsCount);        
        QueueGetTargets();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        float distance = Vector3.Distance(_movable.Position, _currentEnemyTarget);

        if (distance <= _minDistanceToTarget)
        {
            SwitchTarget();
        }

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_movable is CharacterAgent agent)
        {
            agent.SetDestination(_currentEnemyTarget);

            if (agent.CurrentVelocity.sqrMagnitude > 0.01f)
                _rotatable.SetRotationDirection(agent.CurrentVelocity);

            return;
        }

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