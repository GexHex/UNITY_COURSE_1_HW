using UnityEngine;
using UnityEngine.AI;

public class CharacterAgent : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamageable, IJumpable, IHealable
{
    [SerializeField] private int _startHealth = 150;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;

    private NavMeshAgent _navMehsAgent;
    private MoverService _mover;
    private RotatorService _rotator;
    private JamperService _jumper;
    private Health _health;

    public Vector3 CurrentVelocity => _mover.CurrentVelosity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 Position => transform.position;
    public bool InJumpProcess => _jumper.InProcess;
    public int Health => _health.Value;

    private void Awake()
    {
        _navMehsAgent = GetComponent<NavMeshAgent>();
        _navMehsAgent.updateRotation = false;

        _health = new Health(_startHealth);
        _mover = new MoverService(_navMehsAgent, _moveSpeed);
        _rotator = new RotatorService(transform, _rotationSpeed);
        _jumper = new JamperService(_jumpSpeed, _navMehsAgent, this, _jumpCurve);
    }
    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_navMehsAgent.isOnOffMeshLink)
        {
            offMeshLinkData = _navMehsAgent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void TakeDamage(int damage) => _health.TakeDamage(damage);
    public void Heal(int value) => _health.Heal(value);

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetMoveDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetRotationDirection(inputDirection);
    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
}