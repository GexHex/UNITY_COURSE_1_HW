using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private CharacterAgent _characterAgent;
    [SerializeField] private Camera _camera;                  
    [SerializeField] private CameraRayHitPointService _cameraRayHitPointService;
    [SerializeField] private GameObject _flagPrefab;

    private FlagSpawner _flagSpawner;
    private float _timeToChangeStrategy = 5;

    private ControllerSwitchLogic _controllerSwithAgentLogic;

    private void Start()
    {
        _cameraRayHitPointService = new CameraRayHitPointService(_camera);

        _controllerSwithAgentLogic = new ControllerSwitchLogic(_userInput, _characterAgent, _camera, _timeToChangeStrategy, _cameraRayHitPointService);
        _controllerSwithAgentLogic.Start();
        _controllerSwithAgentLogic.Enable();

        _flagSpawner = new FlagSpawner(_flagPrefab, _cameraRayHitPointService, _userInput);
    }

    private void Update()
    {
        _controllerSwithAgentLogic.Update(Time.deltaTime);
        _flagSpawner.Update();
    }
}