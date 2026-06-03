using UnityEngine;

public class Example : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private EnemySpawner _spawner;

    [Header("Configs")]
    [SerializeField] private ConfigOrk[] _orkConfigs;
    [SerializeField] private ConfigElf[] _elfConfigs;
    [SerializeField] private ConfigDragon[] _dragonConfigs;

    private void Start()
    {
        SpawnEnemies(_orkConfigs, 0);
        SpawnEnemies(_elfConfigs, 3);
        SpawnEnemies(_dragonConfigs, 6);
    }

    private void SpawnEnemies<TConfig>(TConfig[] configs, float zOffset) where TConfig : BaseConfig
    {
        for (int i = 0; i < configs.Length; i++)
        {
            TConfig randomConfig = configs[Random.Range(0, configs.Length)];

            Vector3 position = new Vector3(i * 2, 0, zOffset);

            _spawner.Spawn(randomConfig, position);
        }
    }
}