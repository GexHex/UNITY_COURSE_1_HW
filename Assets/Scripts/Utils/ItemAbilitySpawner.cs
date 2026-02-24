using System.Collections.Generic;
using UnityEngine;

public class ItemAbilitySpawner : MonoBehaviour
{
    public List<Vector3> _spawnPoints = new List<Vector3>();

    [SerializeField] private List<Transform> _items;

    void Start()
    {
        foreach (ItemSpawnPointMarker child in GetComponentsInChildren<ItemSpawnPointMarker>())
        {
            _spawnPoints.Add(child.transform.position);
        }

        SpawnPoint();
    }

    private void SpawnPoint()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)           
            Instantiate(_items[Random.Range(0, _items.Count)], _spawnPoints[i], Quaternion.identity);       
    }
}