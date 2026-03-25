using UnityEngine;

public class MineView : MonoBehaviour
{   
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] public GameObject _damageRadiusVisual;

    public void SetDamageScaleView(float scale)
    {      
        _damageRadiusVisual.transform.localScale = new Vector3(scale, 0.1f, scale);
    }
    public void InstantiateEffect(Vector3 positionEffect)
    {        
        Instantiate(_explosionVFX, positionEffect, Quaternion.identity);        
    }  
}