using System.Collections.Generic;
using UnityEngine;

public class MobCheckPoints : MonoBehaviour
{
    private List<Transform> _allTransform = new List<Transform>();

    public List<Transform> AllTransform()
    {
        return _allTransform;
    }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _allTransform.Add(child);
        }        
    } 
}