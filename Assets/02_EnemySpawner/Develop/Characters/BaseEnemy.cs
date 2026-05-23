using UnityEngine;

public abstract class BaseEnemy<TConfig> : MonoBehaviour where TConfig : EnemyConfig
{
    public abstract void Setup(TConfig config);
}