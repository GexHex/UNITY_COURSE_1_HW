using UnityEngine;
using UnityEngine.AI;

public class ControllerSwitchLogic : ControllerBase
{
    private UserInput _userInput;
    private Character _character;

    private ControllerBase _currentController;
    private ControllerBase _onMouseController;
    private ControllerBase _navMeshRandomMoveController;
    private CameraRayHitPointService _cameraRayHitPointService;

    private float _time;
    private float _timeToChangeStrategy;
    private float _timeCooldown;
    private float _timeAnimationCooldown = 1f;
    private int _currentCharacterHealth;

    public ControllerSwitchLogic(UserInput userInput, Character character, Camera camera, float timeToChangeStrategy, CameraRayHitPointService cameraRayHitPointService)
    {
        _userInput = userInput;
        _character = character;
        _timeToChangeStrategy = timeToChangeStrategy;
        _cameraRayHitPointService = cameraRayHitPointService;
    }

    public void Start()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _onMouseController = new ControllerOnMouseDirectionalMovableRotatable(_character.Movable, _character.Rotatable, queryFilter, _cameraRayHitPointService);
        _navMeshRandomMoveController = new ControllerNavMeshRandomDirectionalMovableRotatable(_character.Movable, _character.Rotatable, queryFilter);

        SetCurrentController(_onMouseController);
    }

    protected override void UpdateLogic(float deltaTime)
    {
        SetMove(Time.deltaTime);

        if (_userInput.IsMouseDown())
        {
            _cameraRayHitPointService.Update();
            SetCurrentController(_onMouseController);
            _time = 0;
        }

        SetStrategy();

        _currentController?.Update(Time.deltaTime);
    }

    private void SetStrategy()
    {
        if (_character.CurrentVelocity.sqrMagnitude < 0.01f)
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

    private void SetCurrentController(ControllerBase controller)
    {
        if (_currentController == controller)
            return;

        _currentController?.Disable();
        _currentController = controller;
        _currentController.Enable();
    }

    private void SetMove(float deltaTime)
    {
        if (_currentCharacterHealth != _character.Health)
        {
            _timeCooldown += deltaTime;

            if (_timeCooldown < _timeAnimationCooldown)
            {
                _currentController.Disable();
                _currentController.StopMove();
            }
            else
            {
                _timeCooldown = 0;
                _currentController.Enable();
                _currentCharacterHealth = _character.Health;
            }
        }

        if (_currentCharacterHealth <= 0)
        {
            _currentController.Disable();
            _currentController.StopMove();
        }
    }

    public override void StopMove()
    {
        
    }
}