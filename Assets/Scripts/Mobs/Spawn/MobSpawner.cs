using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{  
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
            mob.Add();            
        }    
    }
}