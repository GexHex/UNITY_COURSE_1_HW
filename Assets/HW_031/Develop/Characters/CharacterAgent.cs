using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAgent : MonoDestroyable, IMovable, IJumper, ICanSpawn, IDamageable
{
    public event Action<CharacterAgent> Died;

    [SerializeField] private CharacterMaterialView _characterMaterialView;

    private NavMeshAgent _agent;
    private AgentMover _mover;
    private TransformDirectionalRotator _rotator;
    private AgentJumper _jumper;
    private Timer _spawnTimer;
    private ReactiveHealth _health;
    private DamageTrigger _damageTrigger;
    private float _timeToSpawn;    

    public Vector3 CurrentVelocity => _mover.CurrentVelosity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public bool InJumpProcess => _jumper.InProcess;

    public float TimeToSpawn => _spawnTimer.Timelimit;

    public void Initialize(
        NavMeshAgent agent,
        AgentMover mover,
        TransformDirectionalRotator rotator,
        AgentJumper jumper,
        Timer spawnTimer,
        float timeToSpawn,
        int health)
    {
        _agent = agent;
        _mover = mover;
        _rotator = rotator;
        _jumper = jumper;
        _spawnTimer = spawnTimer;
        _timeToSpawn = timeToSpawn;
        _spawnTimer.StartProcess(_timeToSpawn);

        _health = new ReactiveHealth(health);
        _health.Died += OnHealthDied;

        _damageTrigger = GetComponentInChildren<DamageTrigger>();
        _damageTrigger.TriggerEntered += OnTriggerEntered;

        _characterMaterialView.Initialize(_health);

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();
    }

    private void OnHealthDied()
    {
        Died?.Invoke(this);
    }

    private void OnDestroy()
    {
        _damageTrigger.TriggerEntered -= OnTriggerEntered;
        _health.Died -= OnHealthDied;
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public bool IsOnNavMeshLikn(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public bool InSpawnProcess(out float elepsedTime) => _spawnTimer.InProcess(out elepsedTime);

    private void OnTriggerEntered(Collider other)
    {
        if (other.CompareTag("PlayerDamageArea"))
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(10, other.name);
            }
        }
    }

    public void TakeDamage(int damage, string damageObject)
    {
        _health.TakeDamage(damage, this.name);
    }
}