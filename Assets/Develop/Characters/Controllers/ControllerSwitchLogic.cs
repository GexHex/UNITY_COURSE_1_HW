using UnityEngine;
using UnityEngine.AI;

public class ControllerSwitchLogic : Controller
{
    private UserInput _userInput;
    private CharacterAgent _characterAgent;
    private Controller _currentController;
    private Controller _navMeshRandomMoveController;
    private CameraRayHitPointService _cameraRayHitPointService;
    private ControllerMouseClick _controller_MouseClick_Agent;
    private float _time;
    private float _timeToChangeStrategy;
    private float _timeCooldown;
    private float _timeAnimationCooldown = 1f;
    private int _tempHealthValue;
    private bool _isHit;

    public ControllerSwitchLogic(UserInput userInput, CharacterAgent character, Camera camera, float timeToChangeStrategy, CameraRayHitPointService cameraRayHitPointService)
    {
        _userInput = userInput;
        _characterAgent = character;
        _timeToChangeStrategy = timeToChangeStrategy;
        _cameraRayHitPointService = cameraRayHitPointService;
    }

    public void Start()
    {
        _tempHealthValue = _characterAgent.Health;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _controller_MouseClick_Agent = new ControllerMouseClick(_characterAgent, _characterAgent, _characterAgent, _cameraRayHitPointService);       
        _navMeshRandomMoveController = new ControllerRandomPoints(_characterAgent, _characterAgent, queryFilter);

        SetCurrentController(_controller_MouseClick_Agent);
    }

    protected override void UpdateLogic(float deltaTime)
    {
        SetMove(Time.deltaTime);

        if (_userInput.IsMouseDown())
        {
            _cameraRayHitPointService.Update();
            SetCurrentController(_controller_MouseClick_Agent);
            _time = 0;
        }

        SetStrategy();

        _currentController?.Update(Time.deltaTime);
    }

    private void SetStrategy()
    {
        if (_isHit)
            return;

        if (_characterAgent.CurrentVelocity.sqrMagnitude < 0.01f)
        {
            _time += Time.deltaTime;

            if (_time > _timeToChangeStrategy && _currentController != _navMeshRandomMoveController)
            {
                SetCurrentController(_navMeshRandomMoveController);
            }
        }
        else
        {
            _time = 0;
        }
    }

    private void SetCurrentController(Controller controller)
    {
        if (_currentController == controller)
            return;

        if (_characterAgent.Health <= 0)
            return;

        _currentController?.Disable();
        _currentController = controller;
        _currentController.Enable();
    }

    private void SetMove(float deltaTime)
    {
        if (_characterAgent.Health <= 0)
        {
            _currentController.Disable();
            _currentController.StopMove();
            return;
        }

        if (_characterAgent.Health < _tempHealthValue && _isHit == false)
        {
            _isHit = true;
            _timeCooldown = 0;

            _currentController.Disable();
            _currentController.StopMove();
        }

        if (_isHit)
        {
            _timeCooldown += deltaTime;

            if (_timeCooldown >= _timeAnimationCooldown)
            {
                _isHit = false;
                _currentController.Enable();
            }
        }

        _tempHealthValue = _characterAgent.Health;
    }

    public override void StopMove()
    {
        _currentController.StopMove();
    }
}