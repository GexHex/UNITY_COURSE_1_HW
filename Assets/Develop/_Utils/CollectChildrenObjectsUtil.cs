using System.Collections.Generic;
using UnityEngine;

public class CollectChildrenObjectsUtil : MonoBehaviour
{
    public List<Mine> _mines;

    void Awake()
    {
        _mines = new List<Mine>(GetComponentsInChildren<Mine>());       
    }
}