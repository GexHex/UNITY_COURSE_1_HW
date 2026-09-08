using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public int LevelsCount => _levels.Count;

        public LevelConfig GetBy(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            return _levels[levelIndex];
        }

        public LevelConfig GetRandom()
        {
            int index = Random.Range(0, _levels.Count);

            return _levels[index];
        }

        public int GetRandomLevelNumber()
        {
            return Random.Range(1, _levels.Count + 1);
        }
    }
}
