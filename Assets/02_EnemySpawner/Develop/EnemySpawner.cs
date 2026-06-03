using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Ork _orkPrefab;
    [SerializeField] private Elf _elfPrefab;
    [SerializeField] private Dragon _dragonPrefab;

    public BaseEnemy Spawn(BaseConfig config, Vector3 position)
    {
        switch (config)
        {
            case ConfigOrk orkConfig:
            {
                Ork ork = Instantiate(_orkPrefab, position, Quaternion.identity);

                ork.Setup(orkConfig);

                return ork;
            }

            case ConfigElf elfConfig:
            {
                Elf elf = Instantiate(_elfPrefab, position, Quaternion.identity);

                elf.Setup(elfConfig);

                return elf;
            }

            case ConfigDragon dragonConfig:
            {
                Dragon dragon = Instantiate(_dragonPrefab, position, Quaternion.identity);

                dragon.Setup(dragonConfig);

                return dragon;
            }

            default: throw new ArgumentException($"Неизвестный конфиг {config.GetType()}");
        }
    }
}