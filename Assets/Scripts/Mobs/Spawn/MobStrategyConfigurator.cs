using UnityEngine;

public class MobStrategyConfigurator : MonoBehaviour
{
    [SerializeField] private MobBehaviorTypes _idleType;
    [SerializeField] private MobBehaviorTypes _aggroType;
    [SerializeField] private Mob _mobPrefab;
    [SerializeField] private MobCheckPoints _checkPoints;
    [SerializeField] private GameObject _deadVFX; 

    private Mob _newMob;

    public void Add(Player player)
    {      
        _newMob = Instantiate(_mobPrefab, transform.position, Quaternion.identity);
        _newMob.SetStrategy_Idle(GetStrategyIdle(player));
        _newMob.SetStrategy_Aggro(GetStrategyAggro(player));
    }

    public IMobBehavior GetStrategyIdle(Player player)
    {
        if (_idleType == MobBehaviorTypes.IdleIdle)
            return new MobBehaviorIdle(_newMob);
        if (_idleType == MobBehaviorTypes.IdleRandomWay)
            return new MobBehaviorRandomWay(_newMob.transform);
        if (_idleType == MobBehaviorTypes.IdleWalkPoints)
            return new MobBehaviorWalkPoints(_newMob.transform, _checkPoints.AllTransform());

        return null;
    }

    public IMobBehavior GetStrategyAggro(Player player)
    {
        if (_aggroType == MobBehaviorTypes.AggroRevese)
            return new MobBehaviorRevese(_newMob.transform, player.transform);
        if (_aggroType == MobBehaviorTypes.AggroFollow)
            return new MobBehaviorFollow(_newMob.transform, player.transform);
        if (_aggroType == MobBehaviorTypes.AggroAnnihilation)
            return new MobBehaviorAnnihilation(_newMob, _deadVFX);

        return null;
    }   
}