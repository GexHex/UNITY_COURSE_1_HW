using UnityEngine;
using UnityEngine.AI;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CollectChildrenObjectsUtil _minesObject;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _flag;
    private FlagCameraRaySpawner _flagSpawner;
    private OnMouseDirectionalMovableRotatableController _onMouseController;
    private MineController _mineController;
    private float _time;
    private float _changeAnimationCooldown = 1f;
    private int _currentCharacterHealth;

    private void Awake()
    {
        _flagSpawner = new FlagCameraRaySpawner(_camera, _flag);
        _currentCharacterHealth = _character.Health;
    }

    private void Start()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _onMouseController = new OnMouseDirectionalMovableRotatableController(_character, _character, queryFilter);
        _onMouseController.Enable();

        _mineController = new MineController(_minesObject._mines, _character);
        _mineController.Enable();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _flagSpawner.Update();
        }

        MoveCharacter(Time.deltaTime);
        _onMouseController.Update(Time.deltaTime);
        _mineController.Update(Time.deltaTime);    
    }

    private void MoveCharacter(float deltaTime)
    {
        if (_character.Health > 0)
            _onMouseController.UpdateTarget(_flagSpawner.RayPosition);

        if (_currentCharacterHealth != _character.Health)
        {
            _time += deltaTime;

            if (_time < _changeAnimationCooldown)
            {
                _onMouseController.SetMove(false);
            }
            else
            {
                _time = 0;               
                _onMouseController.SetMove(true);
                _currentCharacterHealth = _character.Health;
            }
        }

        if (_currentCharacterHealth <= 0)
        {
            _onMouseController.SetMove(false);
        }
    }
}