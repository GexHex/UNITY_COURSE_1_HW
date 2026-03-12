using UnityEngine;

public class MobBehaviorFollow : IMobBehavior
{
    private float _speed = 3f;
    private Transform _player;
    private Transform _mob;
    private Renderer _renderer;

    public MobBehaviorFollow(Transform mob, Transform player)
    {
        _mob = mob;
        _player = player;
        _renderer = mob.GetComponentInChildren<MeshRenderer>();
    }

    public void StartStrategy()
    {
        _renderer.material.color = Color.red;
        Debug.Log($"Start strategy {this}");
        _mob.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void UpdateStrategy()
    {
        _mob.transform.Translate((_player.transform.position - _mob.transform.position).normalized * _speed * Time.deltaTime, Space.World);
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;
        _mob.localScale = new Vector3(1, 1, 1);
    }
}