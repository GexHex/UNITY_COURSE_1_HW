using UnityEngine;
using UnityEngine.AI;

public class ControllerMouseClick : Controller
{
    [SerializeField] private CameraRayHitPointService _cameraRayHitPointService;
    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;
    private IJumpable _jumpable;
    private Vector3 _target;

    public ControllerMouseClick(IDirectionalMovable movable, IDirectionalRotatable rotatable, IJumpable jumpable,CameraRayHitPointService cameraRayHitPointService)
    {
        _movable = movable;
        _rotatable = rotatable;
        _jumpable = jumpable;
        _cameraRayHitPointService = cameraRayHitPointService;
    }

    public override void StopMove()
    {
        
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if(!_cameraRayHitPointService.HasHitPoint)
            return;

        _target = _cameraRayHitPointService.RayHitPoint;
        _movable.SetMoveDirection(_target);     

        if (_jumpable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if (_jumpable.InJumpProcess == false)
            {
                _rotatable.SetRotationDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);
                _jumpable.Jump(offMeshLinkData);
            }
            return;
        }

        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}