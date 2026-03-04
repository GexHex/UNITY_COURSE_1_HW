using System.Collections.Generic;
using UnityEngine;

public class MobIdleStrategyIdle : IMobBehaviorIdle  
{   
    public void BehaviorIdle()
    {
        //Ничего не делает
    }
}

public class MobIdleStrategyRandomWay : IMobBehaviorIdle
{
    private Mob _mob;
    private float _speed = 1f;
    private Vector3 _direction;
    private float _timer;
    private bool _flagOnlyOneStart = true;    

    public MobIdleStrategyRandomWay(Mob mob, PlayerMarker player)
    {
        _mob = mob;
    }

    public void BehaviorIdle()
    {
        if (_flagOnlyOneStart)
            ChangeDirection();   
        
        _flagOnlyOneStart = false;   

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
}

public class MobIdleStrategyWalkPoints : IMobBehaviorIdle
{
    private MobCheckPoints _checkPoints;

    private List<Transform> _checkPointsTransform = new List<Transform>();
    private Queue<Vector3> _checkPointsQueue;
    private Vector3 _currentEnemyTarget;
    private float _minDistanceToTarget = 0.1f;  
    private Mob _mob;

    private float _speed = 5;
    private float _rotateSpeed = 800;

    public MobIdleStrategyWalkPoints(Mob mob, MobCheckPoints checkPoints)
    {
        _mob = mob;
        _checkPoints = checkPoints;
        Start();
    }  

    public void Start()
    {
        _checkPointsTransform = _checkPoints._allTransform;
        _checkPointsQueue = new Queue<Vector3>();

        QueueGetTargets();
    }

    public void BehaviorIdle()
    {
        Vector3 direction = _currentEnemyTarget - _mob.transform.position;
        Vector3 directionNormalized = direction.normalized;

        if (direction.magnitude <= _minDistanceToTarget)
            SwitchTarget();

        ProcessMoveTo(directionNormalized);
        ProcessRotateTo(directionNormalized);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        _mob.transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private void ProcessRotateTo(Vector3 direction)
    {
        Quaternion LookRotation = Quaternion.LookRotation(direction);
        float step = _rotateSpeed * Time.deltaTime;
        _mob.transform.rotation = Quaternion.RotateTowards(_mob.transform.rotation, LookRotation, step);
    }

    private void SwitchTarget()
    {
        _currentEnemyTarget = _checkPointsQueue.Dequeue();
        _checkPointsQueue.Enqueue(_currentEnemyTarget);
    }

    private void QueueGetTargets()
    {
        foreach (Transform targets in _checkPointsTransform)
            _checkPointsQueue.Enqueue(targets.position);

        _currentEnemyTarget = _checkPointsQueue.Peek();
       //transform.position = _checkPointsQueue.Peek(); // Моб сразу на маршруте
    }
}

//-----------------------------------------AggroStrategies-----------------------------------------

public class MobAggroStrategyRevese : IMobBehaviorAggro
{    
    private Mob _mob;
    private PlayerMarker _player;
    private float _speed = 5f;

    public MobAggroStrategyRevese(Mob mob, PlayerMarker player)
    {
        _mob = mob;
        _player = player;
    }

    public void BehaviorAggro()
    {
        _mob.transform.Translate(-(_player.transform.position - _mob.transform.position).normalized * _speed * Time.deltaTime, Space.World);
    }
}

public class MobAgrroStrategyFollow : IMobBehaviorAggro
{
    private float _speed = 3f;
    private PlayerMarker _player;
    private Mob _mob;

    public MobAgrroStrategyFollow(Mob mob, PlayerMarker player)
    {
        _mob = mob;
        _player = player;
    }

    public void BehaviorAggro()
    {
        _mob.transform.Translate((_player.transform.position - _mob.transform.position).normalized * _speed * Time.deltaTime, Space.World);
    }
}

public class MobAgrroStrategyAnnihilation : IMobBehaviorAggro
{
    private Mob _mob;
    private GameObject _vfxDead;

    public MobAgrroStrategyAnnihilation(Mob mob, GameObject VFXDead)
    {
        _mob = mob;
        _vfxDead = VFXDead;
    }

    public void BehaviorAggro()
    {
        if (_mob != null)
        {
            Object.Destroy(_mob.gameObject);
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        GameObject.Instantiate(_vfxDead, _mob.transform.position, Quaternion.identity);       
    }
}