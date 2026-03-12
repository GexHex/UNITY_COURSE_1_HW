using UnityEngine;

public class MobBehaviorRevese : IMobBehavior
{    
    private Transform _mob;
    private Transform _player;
    private float _speed = 5f;
    private Renderer _renderer;

    public MobBehaviorRevese(Transform mob, Transform player)
    {
        _mob = mob;
        _player = player;
        _renderer = mob.GetComponentInChildren<MeshRenderer>();
    }

    public void StartStrategy()
    {
        Debug.Log($"Start strategy {this}");
        _renderer.material.color = Color.red;       
        _mob.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void UpdateStrategy()
    {
        _mob.transform.Translate(-(_player.transform.position - _mob.transform.position).normalized * _speed * Time.deltaTime, Space.World);
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;
        _mob.localScale = new Vector3(1, 1, 1);
    }
}