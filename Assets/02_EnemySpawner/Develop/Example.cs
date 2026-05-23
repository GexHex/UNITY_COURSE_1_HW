using UnityEngine;

public class Example : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private EnemySpawner _spawner;

    [Header("Prefabs")]
    [SerializeField] private Ork _orkPrefab;
    [SerializeField] private Elf _elfPrefab;
    [SerializeField] private Dragon _dragonPrefab;

    [Header("Configs")]
    [SerializeField] private ConfigOrk[] _orkConfigs;
    [SerializeField] private ConfigElf[] _elfConfigs;
    [SerializeField] private ConfigDragon[] _dragonConfigs;

    private void Start()
    {
        SpawnEnemies(_orkPrefab, _orkConfigs, 0);
        SpawnEnemies(_elfPrefab, _elfConfigs, 3);
        SpawnEnemies(_dragonPrefab, _dragonConfigs, 6);
    }

    private void SpawnEnemies<TEnemy, TConfig>(TEnemy prefab, TConfig[] configs, float zOffset) 
        where TEnemy : BaseEnemy<TConfig>
        where TConfig : EnemyConfig
    {
        for (int i = 0; i < configs.Length; i++)
        {
            TConfig randomConfig = configs[Random.Range(0, configs.Length)];

            Vector3 position = new Vector3(i * 2, 0, zOffset);

            _spawner.Spawn(prefab, randomConfig, position);
        }
    }
}