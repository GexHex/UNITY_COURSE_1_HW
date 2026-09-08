using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<StageConfig> _stageConfigs;

        [field: SerializeField] public TowerConfig TowerConfig { get; private set; }
        [field: SerializeField, Min(1)] public float TowerMaxHealth { get; private set; } = 100;
        [field: SerializeField, Min(0)] public int RewardGold { get; private set; } = 50;
        [field: SerializeField, Min(0)] public float RestDuration { get; private set; } = 8f;
        [field: SerializeField] public ExplosionConfig ExplosionConfig { get; private set; }
        [field: SerializeField] public MineConfig MineConfig { get; private set; }

        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
    }
}
