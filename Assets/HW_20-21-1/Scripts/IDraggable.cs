using UnityEngine;

public interface IDraggable : IRigidbodyObject
{
    public Vector3 Position { get; }

    public void Move(Vector3 position);   
}