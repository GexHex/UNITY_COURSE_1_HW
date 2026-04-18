using UnityEngine;

public class HealView : MonoBehaviour
{   
    [SerializeField] private GameObject _healVFX;    
    
    public void InstantiateEffect(Vector3 positionEffect)
    {        
        Instantiate(_healVFX, positionEffect, Quaternion.identity);        
    }
}