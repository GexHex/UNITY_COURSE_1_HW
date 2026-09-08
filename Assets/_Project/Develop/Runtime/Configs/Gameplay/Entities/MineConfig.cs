using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewMineConfig", fileName = "MineConfig")]
    public class MineConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Mine";
        [field: SerializeField, Min(0)] public int Cost { get; private set; } = 10;
        [field: SerializeField, Min(0)] public float TriggerRadius { get; private set; } = 1.2f;
        [field: SerializeField, Min(0)] public float ExplosionRadius { get; private set; } = 2.5f;
        [field: SerializeField, Min(0)] public float ExplosionDamage { get; private set; } = 50;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 0.2f;
    }
}
