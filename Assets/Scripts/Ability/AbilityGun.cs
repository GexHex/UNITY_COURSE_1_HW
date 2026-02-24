using UnityEngine;

[System.Serializable]
public class AbilityGun : AbilityBase
{
    private int _value = 1;   

    public override int UseAbility()
    {        
        return _value;
    }
}