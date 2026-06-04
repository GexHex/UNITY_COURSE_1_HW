using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/BulletConfig", fileName = "BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [field: SerializeField] public GameObject DeadVFX { get; private set; }
    [field: SerializeField] public float Speed { get; private set; } = 10f;
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public float TimeToExplosion { get; private set; } = 1f;
}