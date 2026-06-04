using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterRandomPointsController : Controller
{
    private CharacterAgent _character;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private NavMeshQueryFilter _queryFilter;

    [Header("Settings")]
    private float _minDistanceToTarget = 0.3f;
    private float _radius = 5f;
    private int _checkpointsCount = 5;

    public List<Vector3> CheckPoints { get; private set; }
    private Queue<Vector3> _checkPointsQueue = new Queue<Vector3>();
    private Vector3 _currentTarget;

    public AgentCharacterRandomPointsController(
        CharacterAgent character,
        float radius = 5f,
        int checkpointsCount = 5)
    {
        _character = character;
        _radius = radius;
        _checkpointsCount = checkpointsCount;

        _queryFilter = new NavMeshQueryFilter
        {
            agentTypeID = 0,
            areaMask = NavMesh.AllAreas
        };

        GetRandomCheckpointsAround();
    }

    public override void Enable()
    {
        base.Enable();
        GetRandomCheckpointsAround();
    }

    public void GetRandomCheckpointsAround()
    {
        _checkPointsQueue.Clear();

        CheckPoints = NavMeshUtils.GetRandomPoints(_character.transform.position, _radius, _checkpointsCount);

        QueueGetTargets();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_character.InSpawnProcess(out float elapsedTime))
            return;

        if (_character.IsOnNavMeshLikn(out OffMeshLinkData offMeshLinkData))
        {
            if (_character.InJumpProcess == false)
            {
                _character.SetRotationDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);
                _character.Jump(offMeshLinkData);
            }
            return;
        }

        if (CheckPoints == null || CheckPoints.Count == 0) return;

        float distance = Vector3.Distance(_character.transform.position, _currentTarget);

        if (distance <= _minDistanceToTarget)
        {
            SwitchTarget();
        }

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        _character.SetDestination(_currentTarget);

        if (_character.CurrentVelocity.sqrMagnitude > 0.01f)
        {
            _character.SetRotationDirection(_character.CurrentVelocity);
        }
    }

    private void SwitchTarget()
    {
        if (_checkPointsQueue.Count == 0) return;

        Vector3 oldTarget = _checkPointsQueue.Dequeue();
        _checkPointsQueue.Enqueue(oldTarget);

        _currentTarget = _checkPointsQueue.Peek();
    }

    private void QueueGetTargets()
    {
        _checkPointsQueue.Clear();
        foreach (Vector3 point in CheckPoints)
            _checkPointsQueue.Enqueue(point);

        if (_checkPointsQueue.Count > 0)
            _currentTarget = _checkPointsQueue.Peek();
    }
}