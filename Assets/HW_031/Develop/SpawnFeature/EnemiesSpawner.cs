using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;

    public EnemiesSpawner(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public List<CharacterAgent> Spawn(AgentEnemyConfig enemyConfig, Transform target, List<Vector3> spawnPoints)
    {
        List<CharacterAgent> spawnedEnemies = new();

        foreach (Vector3 point in spawnPoints)
        {
            CharacterAgent enemy = _enemiesFactory.CreateAgentEnemy(enemyConfig, point, target);

            spawnedEnemies.Add(enemy);
        }

        return spawnedEnemies;
    }
}