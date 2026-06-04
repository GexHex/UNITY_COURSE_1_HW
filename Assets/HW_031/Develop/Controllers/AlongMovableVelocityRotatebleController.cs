public class AlongMovableVelocityRotatebleController : Controller
{
    private IDirectionalRotatable _rotatable;
    private IDirectionalMovable _movable;

    public AlongMovableVelocityRotatebleController(IDirectionalRotatable rotatable, IDirectionalMovable movable)
    {
        _rotatable = rotatable;
        _movable = movable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}