using UnityEngine;

public class Mob : MonoBehaviour
{
    private IMobBehavior _mobBehavior_Idle;
    private IMobBehavior _mobBehavior_Aggro;
   
    private IMobBehavior _currentStrategy;
    private IMobBehavior _newStrategy;
    private bool _switchStrategy;

    public void SetStrategy_Idle(IMobBehavior iMobBehavior_Idle)
    {
        _mobBehavior_Idle = iMobBehavior_Idle;
    }

    public void SetStrategy_Aggro(IMobBehavior iMobBehavior_Aggro)
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
            _newStrategy = _mobBehavior_Aggro;
        else
            _newStrategy = _mobBehavior_Idle;

        if (_currentStrategy != _newStrategy)
        {
            _currentStrategy?.StopStrategy();   

            _currentStrategy = _newStrategy;

            _currentStrategy.StartStrategy();   
        }

        _currentStrategy.UpdateStrategy();
    }        
}