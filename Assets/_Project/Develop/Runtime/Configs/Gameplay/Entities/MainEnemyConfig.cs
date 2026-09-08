using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewMainEntityConfig", fileName = "MainEnemyConfig")]
    public class MainEnemyConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Entity";
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 4;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 720;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 30;
        [field: SerializeField, Min(0)] public float BodyContactDamage { get; private set; } = 20;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = 1f;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 0.4f;
        [field: SerializeField, Min(0)] public float SpawnProcessTime { get; private set; } = 2;
    }
}
