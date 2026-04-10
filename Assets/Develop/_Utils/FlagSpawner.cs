using UnityEngine;

public class FlagSpawner
{
    private GameObject _flagPrefab;
    private CameraRayHitPointService _cameraRayHitPointService;
    private UserInput _userInput;
    private GameObject _flag;
    private Vector3 _flagPosition;

    public FlagSpawner(GameObject flagPrefab, CameraRayHitPointService cameraRayHitPointService, UserInput userInput)
    {
        _flagPrefab = flagPrefab;
        _cameraRayHitPointService = cameraRayHitPointService;
        _userInput = userInput;
    }

    public void Update()
    {
        if (_cameraRayHitPointService == null)
            return;

        if (_userInput.IsMouseDown())
        {
            SetFlag();
        }        
    }

    public void SetFlag()
    {
        Vector3 newPosition = _cameraRayHitPointService.RayHitPoint;

        if (_flag == null)
        {
            SpawnNewFlag(newPosition);
        }

        else if (_flagPosition != newPosition)
        {
            GameObject.Destroy(_flag);
            SpawnNewFlag(newPosition);
        }
    }

    private void SpawnNewFlag(Vector3 position)
    {
        _flag = GameObject.Instantiate(_flagPrefab, position, Quaternion.identity);
        _flagPosition = position;
    }
}