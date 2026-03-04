using UnityEngine;

public class MobStrategyConfigurator : MonoBehaviour
{
    [SerializeField] private MobIdleTypes _idleType;
    [SerializeField] private MobAggroTypes _aggroType;
    [SerializeField] private PlayerMarker _player;
    [SerializeField] private Mob _mobPrefab;
    [SerializeField] private MobCheckPoints _checkPoints;
    [SerializeField] private GameObject _deadVFX;

    private Mob _newMob;

    public void Add()
    {      
        _newMob = Instantiate(_mobPrefab, transform.position, Quaternion.identity);
        _newMob.SetStrategy_Idle(GetStrategyIdle());
        _newMob.SetStrategy_Aggro(GetStrategyAggro());
    }

    public IMobBehaviorIdle GetStrategyIdle()
    {
        if (_idleType == MobIdleTypes.IdleIdle)
            return new MobIdleStrategyIdle();
        if (_idleType == MobIdleTypes.IdleRandomWay)
            return new MobIdleStrategyRandomWay(_newMob, _player);
        if (_idleType == MobIdleTypes.IdleWalkPoints)
            return new MobIdleStrategyWalkPoints(_newMob, _checkPoints);

        return null;
    }

    public IMobBehaviorAggro GetStrategyAggro()
    {
        if (_aggroType == MobAggroTypes.AggroRevese)
            return new MobAggroStrategyRevese(_newMob, _player);
        if (_aggroType == MobAggroTypes.AggroFollow)
            return new MobAgrroStrategyFollow(_newMob, _player);
        if (_aggroType == MobAggroTypes.AggroAnnihilation)
            return new MobAgrroStrategyAnnihilation(this._newMob, _deadVFX);

        return null;
    }   
}