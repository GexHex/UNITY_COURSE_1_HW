using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Stages
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stages/NewWaveStage", fileName = "WaveStage")]
    public class WaveStageConfig : StageConfig
    {
        [field: SerializeField] public MainEnemyConfig EnemyConfig { get; private set; }
        [field: SerializeField, Min(1)] public int EnemiesCount { get; private set; } = 5;
        [field: SerializeField, Min(0)] public float SpawnDistance { get; private set; } = 12f;
    }
}
