using System.Collections.Generic;
using UnityEngine;

public class MobBehaviorWalkPoints : IMobBehavior
{
    private List<Transform> _checkPoints;
    private List<Transform> _checkPointsTransform = new List<Transform>();
    private Queue<Vector3> _checkPointsQueue;
    private Vector3 _currentEnemyTarget;
    private float _minDistanceToTarget = 0.1f;
    private Transform _mob;
    private float _speed = 5;
    private float _rotateSpeed = 800;
    private Renderer _renderer;

    public MobBehaviorWalkPoints(Transform mob, List<Transform> checkPoints)
    {
        _mob = mob;
        _renderer = mob.GetComponentInChildren<MeshRenderer>();
        _checkPoints = checkPoints;
    }

    public void StartStrategy()
    {      
        _checkPointsTransform = _checkPoints;
        _checkPointsQueue = new Queue<Vector3>();
        QueueGetTargets();

        _renderer.material.color = Color.green;
        Debug.Log($"Start strategy {this}");
    }

    public void UpdateStrategy()
    {
        Vector3 direction = _currentEnemyTarget - _mob.transform.position;
        Vector3 directionNormalized = direction.normalized;

        if (direction.magnitude <= _minDistanceToTarget)
            SwitchTarget();

        ProcessMoveTo(directionNormalized);
        ProcessRotateTo(directionNormalized);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        _mob.transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private void ProcessRotateTo(Vector3 direction)
    {
        Quaternion LookRotation = Quaternion.LookRotation(direction);
        float step = _rotateSpeed * Time.deltaTime;
        _mob.transform.rotation = Quaternion.RotateTowards(_mob.transform.rotation, LookRotation, step);
    }

    private void SwitchTarget()
    {
        _currentEnemyTarget = _checkPointsQueue.Dequeue();
        _checkPointsQueue.Enqueue(_currentEnemyTarget);
    }

    private void QueueGetTargets()
    {
        foreach (Transform targets in _checkPointsTransform)
            _checkPointsQueue.Enqueue(targets.position);

        _currentEnemyTarget = _checkPointsQueue.Peek();
       //transform.position = _checkPointsQueue.Peek(); // Моб сразу на маршруте
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;       
    }
}