using UnityEngine;

public class FlagSpawner
{
    private GameObject _flagPrefab;
    private GameObject _flag;
    private Vector3 _flagPosition;

    public FlagSpawner(GameObject flagPrefab)
    {
        _flagPrefab = flagPrefab;
    }

    public void SetFlag(CameraRayHitPointService hitPointService)
    {
        if (hitPointService == null)
            return;

        Vector3 newPosition = hitPointService.RayHitPoint;

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