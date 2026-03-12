using UnityEngine;

public class MobBehaviorIdle : IMobBehavior
{
    private Renderer _renderer;

    public MobBehaviorIdle(Mob mob)
    {
        _renderer = mob.GetComponentInChildren<MeshRenderer>();     
    }

    public void StartStrategy()
    {
        Debug.Log($"Start strategy {this}");
        _renderer.material.color = Color.green;
    }

    public void UpdateStrategy()
    {
        //Ничего не делает
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;
    }
}