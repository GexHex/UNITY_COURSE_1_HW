using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    public int Count { get; private set; }
    private void Awake()
    {
        Count = transform.childCount;          
    }
}