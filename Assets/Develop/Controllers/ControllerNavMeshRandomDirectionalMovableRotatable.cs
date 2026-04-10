using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ControllerNavMeshRandomDirectionalMovableRotatable : ControllerBase
{
    public List<Vector3> _checkPoints { get; private set; }

    private DirectionalMover _movable;
    private DirectionalRotator _rotatable;

    private Queue<Vector3> _checkPointsQueue = new Queue<Vector3>();
    private Vector3 _currentEnemyTarget;   
    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private float _minDistanceToTarget = 0.3f;    
    private float _radius = 5;
    private int _checkpointsCount = 5;

    public ControllerNavMeshRandomDirectionalMovableRotatable(DirectionalMover movable, DirectionalRotator rotatable, NavMeshQueryFilter queryFilter)
    {
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

    public override void StopMove()
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

    protected override void UpdateLogic(float deltaTime)
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

    private void DrawGizmos()
    {
        if (Application.isPlaying)
        {
            foreach (Vector3 targets in _checkPoints)
            {
                Gizmos.DrawSphere(targets, 0.2f);
                Gizmos.color = Color.red;
            }

            for (int i = 0; i < _checkPoints.Count - 1; i++)
            {               
                Gizmos.DrawLine(_checkPoints[i], _checkPoints[i + 1]);
                Gizmos.DrawLine(_checkPoints.Last(), _checkPoints.First());                
            }
        }
    }
}