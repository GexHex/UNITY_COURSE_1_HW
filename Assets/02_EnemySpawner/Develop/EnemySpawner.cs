using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public TEnemy Spawn<TEnemy, TConfig>(TEnemy prefab, TConfig config, Vector3 position) 
        where TEnemy : BaseEnemy<TConfig>
        where TConfig : EnemyConfig
    {
        TEnemy enemy = Instantiate(prefab, position, Quaternion.identity);

        enemy.Setup(config);

        return enemy;
    }
}