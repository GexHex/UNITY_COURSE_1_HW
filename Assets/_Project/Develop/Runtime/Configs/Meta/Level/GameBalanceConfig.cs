using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Level
{
    [CreateAssetMenu(menuName = "Configs/Meta/Level/GameBalanceConfig", fileName = "GameBalanceConfig")]
    public class GameBalanceConfig : ScriptableObject
    {
        [field: SerializeField] public int GoldToWin { get; private set; } = 15;
        [field: SerializeField] public int GoldToLose { get; private set; } = 10;
        [field: SerializeField] public int GoldToReset { get; private set; } = 50;
    }
}