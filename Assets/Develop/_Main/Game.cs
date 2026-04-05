using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private Character _character;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _flag;    
    private FlagSpawner _flagSpawner;
    private CameraRayHitPointService _cameraRayHitPointService; 
    private ControllerCompositeOnMouseDirectionalMovableRotatable _onMouseController;  
    private ControllerCompositeNavMeshRandomMove _navMeshRandomMoveController;
    private ICompositeController _currentController;
    private float _time;
    private float _timeToChangeStrategy = 5;

    private void Awake()
    {
        _cameraRayHitPointService = new CameraRayHitPointService(_camera);
        _flagSpawner = new FlagSpawner(_flag);
    }

    private void Start()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _onMouseController = new ControllerCompositeOnMouseDirectionalMovableRotatable(_character.Movable, _character.Rotatable, queryFilter, _cameraRayHitPointService);
        _navMeshRandomMoveController = new ControllerCompositeNavMeshRandomMove(_character.Movable, _character.Rotatable, queryFilter);

        SetCurrentController(_onMouseController);
        _character.SetController(_currentController);
    }

    private void Update()
    {
        _character.MoveControll(Time.deltaTime);        

        if (_userInput.IsMouseDown())
        {
            _cameraRayHitPointService.Update();                 
            SetCurrentController(_onMouseController);
            SpawnFlag();
            _time = 0;
        }

        ChangeStrategy();

        _currentController?.Update(Time.deltaTime);
    }

    private void ChangeStrategy()
    {
        if (_character.CurrentVelocity.sqrMagnitude < 0.01f)
        {
            _time += Time.deltaTime;

            if (_time > _timeToChangeStrategy && _currentController != _navMeshRandomMoveController)
            {
                _navMeshRandomMoveController.GetRandomCheckpoints();
                SetCurrentController(_navMeshRandomMoveController);
            }
        }
        else
        {
            _time = 0;
        }
    }

    private void SetCurrentController(ICompositeController controller)
    {
        if (_currentController == controller)
            return;

        _currentController?.Disable();
        _currentController = controller;
        _currentController.Enable();
    }

    private void SpawnFlag()
    {
        _flagSpawner.SetFlag(_cameraRayHitPointService);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            foreach (Vector3 targets in _navMeshRandomMoveController._checkPoints)
            {
                Gizmos.DrawSphere(targets, 0.2f);
                Gizmos.color = Color.red;
            }

            for (int i = 0; i < _navMeshRandomMoveController._checkPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(_navMeshRandomMoveController._checkPoints[i], _navMeshRandomMoveController._checkPoints[i + 1]);
                Gizmos.DrawLine(_navMeshRandomMoveController._checkPoints.Last(), _navMeshRandomMoveController._checkPoints.First());
            }
        }
    }     
}