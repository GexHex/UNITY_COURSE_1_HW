using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineSpawner : MonoBehaviour
{
    [SerializeField] private CharacterAgent _spawnCenter;
    [SerializeField] private GameObject _medicinePrefab;
    private List<Vector3> _points;
    private Coroutine _spawnCoroutine;
    private float _spawnRadius = 3;
    private int _spanwCount = 1;    
    private float _spawnTime = 0.5f;    

    private void Update()
    {
        _points = NavMeshUtils.GetRandomPoints(_spawnCenter.transform.position, _spawnRadius, _spanwCount);

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCoroutine();
        }
    }

    private void ToggleCoroutine()
    {
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnProcess());
        }
        else
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            if (_spawnCenter.Health > 0)
            {
                SpawnMedicine();
            }
        }
    }

    private void SpawnMedicine()
    {
        foreach (var point in _points)
        {
            Instantiate(_medicinePrefab, point, Quaternion.identity);
        }
    }
}