using UnityEngine;

public class Elf : BaseEnemy
{
    private int _mana;
    private float _agility;

    public void Setup(ConfigElf config)
    {
        _mana = config.Mana;
        _agility = config.Agility;

        Debug.Log($"{this.name} | Mana: {_mana} Agility: {_agility}");
    }
}