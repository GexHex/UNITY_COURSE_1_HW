using UnityEngine;

public interface IMovable
{
    public Transform Position { get; }
    public void SetRigidbodyProperties(bool set);
    public void Move(Vector3 position);   
}