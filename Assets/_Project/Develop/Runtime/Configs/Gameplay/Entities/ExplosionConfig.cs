using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewExplosionConfig", fileName = "ExplosionConfig")]
    public class ExplosionConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Explosion";
        [field: SerializeField, Min(0)] public float Radius { get; private set; } = 2.5f;
        [field: SerializeField, Min(0)] public float Damage { get; private set; } = 40;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 0.35f;
    }
}
