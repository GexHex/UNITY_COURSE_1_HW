using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Level
{
    [CreateAssetMenu(menuName = "Configs/Meta/Level/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<char> Symbols { get; private set; } = new()
        { 'a', 'b', 'c', 'd', 'e', 'f', 'g' ,'h', 'j', 'k'};
        [field: SerializeField]
        public List<char> Numbers { get; private set; } = new()
        { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
    }
}