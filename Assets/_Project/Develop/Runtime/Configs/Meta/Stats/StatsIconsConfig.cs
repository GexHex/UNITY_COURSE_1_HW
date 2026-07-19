using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Stats
{
    [CreateAssetMenu(menuName = "Configs/Meta/Stats/StatsIconsConfig", fileName = "StatsIconsConfig")]
    public class StatsIconsConfig : ScriptableObject
    {
        [SerializeField] private List<StatsConfig> _configs;

        public Sprite GetSpriteFor(StatsType type)
            => _configs.First(config => config.Type == type).Sprite;

        [Serializable]
        private class StatsConfig
        {
            [field: SerializeField] public StatsType Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}