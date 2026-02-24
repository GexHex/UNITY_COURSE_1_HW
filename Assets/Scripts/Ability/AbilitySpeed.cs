using UnityEngine;

[System.Serializable]
public class AbilitySpeed : AbilityBase
{
    private int _value = 2;   

    public override int UseAbility()
    {        
        return _value;
    }
}