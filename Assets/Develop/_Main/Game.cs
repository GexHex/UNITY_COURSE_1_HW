using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;              
    [SerializeField] private Character _character;             
    [SerializeField] private Camera _camera;                  
    [SerializeField] private CameraRayHitPointService _cameraRayHitPointService;
    [SerializeField] private GameObject _flagPrefab;
    private FlagSpawner _flagSpawner;
    private float _timeToChangeStrategy = 5;
    private ControllerSwitchLogic _controllerMain;   

    private void Awake()
    {
        _cameraRayHitPointService = new CameraRayHitPointService(_camera);

        _controllerMain = new ControllerSwitchLogic(_userInput, _character, _camera, _timeToChangeStrategy, _cameraRayHitPointService);
        _controllerMain.Start();
        _controllerMain.Enable();

        _flagSpawner = new FlagSpawner(_flagPrefab, _cameraRayHitPointService, _userInput);
    }

    private void Update()
    {
        _controllerMain.Update(Time.deltaTime);
        _flagSpawner.Update();
    }
}