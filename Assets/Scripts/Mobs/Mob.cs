using UnityEngine;

public class Mob : MonoBehaviour
{
    private IMobBehaviorIdle _mobBehavior_Idle;
    private IMobBehaviorAggro _mobBehavior_Aggro;
    private bool _switchStrategy;
   
    public void SetStrategy_Idle(IMobBehaviorIdle iMobBehavior_Idle)
    {
        _mobBehavior_Idle = iMobBehavior_Idle;
    }

    public void SetStrategy_Aggro(IMobBehaviorAggro iMobBehavior_Aggro)
    {
        _mobBehavior_Aggro = iMobBehavior_Aggro;
    }

    private void OnTriggerEnter(Collider other)
    {
        _switchStrategy = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _switchStrategy = false;
    }

    private void Update()
    {
        if (_switchStrategy)
            _mobBehavior_Aggro.BehaviorAggro();        
        else
            _mobBehavior_Idle.BehaviorIdle();
    }        
}