using UnityEngine;

public class MobBehaviorRandomWay : IMobBehavior
{
    private Transform _mob;
    private Vector3 _direction;
    private float _speed = 1f;    
    private float _timer;
    private Renderer _renderer;

    public MobBehaviorRandomWay(Transform mob)
    {
        _mob = mob;
        _renderer = _mob.GetComponentInChildren<MeshRenderer>();
    }

    public void StartStrategy()
    {       
        ChangeDirection();

        Debug.Log($"Start strategy {this}");
        _renderer.material.color = Color.green;
    }

    public void UpdateStrategy()
    {
        _timer += Time.deltaTime;

        if (_timer >= 3f)
        {
            ChangeDirection();
            _timer = 0f;
        }

        _mob.transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    void ChangeDirection()
    {        
        _direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;      
    }
}