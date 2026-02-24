using UnityEngine;

[System.Serializable]
public class AbilityHealth : AbilityBase
{
    private int _value = 20;   

    public override int UseAbility()
    {        
        return _value;
    }
}