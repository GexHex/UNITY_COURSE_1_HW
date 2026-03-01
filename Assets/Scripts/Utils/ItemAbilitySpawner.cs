using System.Collections.Generic;
using UnityEngine;

public class ItemAbilitySpawner : MonoBehaviour
{
    [SerializeField] private List<AbilityBase> _items;

    void Start()
    {     
        SpawnPoint();
    }

    private void SpawnPoint()
    {
        foreach (ItemSpawnPointMarker child in GetComponentsInChildren<ItemSpawnPointMarker>())
        {
            Instantiate(_items[Random.Range(0, _items.Count)], child.transform.position, Quaternion.identity);
        }
    }
}