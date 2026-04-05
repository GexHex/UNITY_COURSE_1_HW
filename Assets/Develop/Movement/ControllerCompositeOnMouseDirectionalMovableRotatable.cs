using UnityEngine;
using UnityEngine.AI;

public class ControllerCompositeOnMouseDirectionalMovableRotatable : ICompositeController
{
    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;

    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private CameraRayHitPointService _hitPointService;

    private Vector3 _target;
    private bool _isEnableUpdateLogic;

    public ControllerCompositeOnMouseDirectionalMovableRotatable(IDirectionalMovable movable, IDirectionalRotatable rotatable, NavMeshQueryFilter queryFilter, CameraRayHitPointService hitPoint)
    {
        _movable = movable;
        _rotatable = rotatable;
        _queryFilter = queryFilter;
        _hitPointService = hitPoint;
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

    public void UpdateLogic(float deltaTime)
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