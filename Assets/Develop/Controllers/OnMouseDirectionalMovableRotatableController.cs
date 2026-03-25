using UnityEngine;
using UnityEngine.AI;

public class OnMouseDirectionalMovableRotatableController : Controller
{
    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;
    private NavMeshQueryFilter _queryFilter;
    private Transform _target;
    private NavMeshPath _pathToTarget = new NavMeshPath();
    private bool _canMove = true;

    public OnMouseDirectionalMovableRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _rotatable = rotatable;        
        _queryFilter = queryFilter;
    }

    public void UpdateTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void SetMove(bool set)
    {
        _canMove = set;
    }

    protected override void UpdateLogic(float delaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);

        if (_target != null && _canMove == false)
        {
            _movable.SetMoveDirection(Vector3.zero);
        }

        if (_target != null && _canMove == true)
        {
            if (NavMeshUtils.TryGetPath(_movable.Position, _target, _queryFilter, _pathToTarget))
            {
                float distanceToTarget = NavMeshUtils.GetPathLenght(_pathToTarget);

                if (_pathToTarget.corners.Length >= 2)
                {
                    _movable.SetMoveDirection(_pathToTarget.corners[1] - _pathToTarget.corners[0]);
                }

                if (NavMeshUtils.GetPathLenght(_pathToTarget) < 1)
                    _movable.SetMoveDirection(Vector3.zero);
            }
        }
    }
}