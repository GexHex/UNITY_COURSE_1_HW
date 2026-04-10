using UnityEngine;
using UnityEngine.AI;

public class ControllerOnMouseDirectionalMovableRotatable : ControllerBase
{
    private DirectionalMover _movable;
    private DirectionalRotator _rotatable;

    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private CameraRayHitPointService _hitPointService;

    private Vector3 _target;

    public ControllerOnMouseDirectionalMovableRotatable(DirectionalMover movable, DirectionalRotator rotatable, NavMeshQueryFilter queryFilter, CameraRayHitPointService hitPoint)
    {
        _movable = movable;
        _rotatable = rotatable;
        _queryFilter = queryFilter;
        _hitPointService = hitPoint;
    }

    public override void StopMove()
    {
        _movable.CurrentVelocity = new Vector3(0, 0, 0);
        _movable.SetMoveDirection(Vector3.zero);
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _target = _hitPointService.RayHitPoint;

        _rotatable.SetRotationDirection(_movable.CurrentVelocity);

        if (_hitPointService != null)
        {
            _movable.SetMoveDirection(Vector3.zero);
        }

        if (_hitPointService != null)
        {
            if (NavMeshUtils.TryGetPath(_movable.Position, _target, _queryFilter, _pathToTarget))
            {
                float distanceToTarget = NavMeshUtils.GetPathLenght(_pathToTarget);

                if (_pathToTarget.corners.Length >= 2)
                {
                    _movable.SetMoveDirection(_pathToTarget.corners[1] - _pathToTarget.corners[0]);
                }

                if (distanceToTarget < 1)
                    _movable.SetMoveDirection(Vector3.zero);
            }
        }
    }
}