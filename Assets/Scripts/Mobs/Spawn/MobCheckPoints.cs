using System.Collections.Generic;
using UnityEngine;

public class MobCheckPoints : MonoBehaviour
{
    public List<Transform> _allTransform = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _allTransform.Add(child);
        }        
    } 
}