using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] Player _player;
    private List<MobStrategyConfigurator> _allMobs = new List<MobStrategyConfigurator>();

    private void Awake()
    {
        _allMobs.AddRange(GetComponentsInChildren<MobStrategyConfigurator>());
        AddMobs();
    }

    private void AddMobs()
    {
        foreach (MobStrategyConfigurator mob in _allMobs)
        {
            mob.Add(_player);            
        }    
    }
}