using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewTowerConfig", fileName = "TowerConfig")]
    public class TowerConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Tower";
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 1.5f;
    }
}
