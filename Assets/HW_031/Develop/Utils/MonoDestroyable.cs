using System;
using UnityEngine;

public class MonoDestroyable : MonoBehaviour
{
    public event Action Destroyed;
    public bool IsDestroyed {  get; private set; }

    public void DestroySelf()
    {
        if (IsDestroyed)
            return;

        IsDestroyed = true;

        Destroyed?.Invoke();

        Destroy(gameObject);
    }
}