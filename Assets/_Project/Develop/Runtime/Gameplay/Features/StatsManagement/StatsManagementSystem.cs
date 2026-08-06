using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    internal class StatsManagementSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _runTeleportEvent;
        private ReactiveEvent _statsTimerEvent;
        private ReactiveEvent _fullOfEnergyEvent;

        private ReactiveVariable<int> _energy;

        private IDisposable _runTeleportDisposable;
        private IDisposable _statsTimerDisposable;

        private int _maxEnergy = 110;
        private int _minEnergy = 0;
        private int _increaseEnergyValue;


        public void OnInit(Entity entity)
        {
            _energy = entity.Energy;
            _runTeleportEvent = entity.RunTeleportEvent;
            _statsTimerEvent = entity.StatsTimerEvent;
            _fullOfEnergyEvent = entity.FullOfEnergyEvent;

            _runTeleportDisposable = _runTeleportEvent.Subscribe(DecreaseEnegy);
            _statsTimerDisposable = _statsTimerEvent.Subscribe(IncreaseEnergy);          

            _increaseEnergyValue = _maxEnergy / 10;
        }

        public void OnDispose()
        {
            _runTeleportDisposable.Dispose();
            _statsTimerDisposable.Dispose();
        }

        private void DecreaseEnegy()
        {
            _energy.Value -= 10;

            if (_energy.Value <= _minEnergy)
                _energy.Value = 0;

            Debug.Log($"Уменьшение энергии: {_energy.Value}");
        }

        private void IncreaseEnergy()
        {
            _energy.Value += _increaseEnergyValue;

            if (_energy.Value >= _maxEnergy)
            {
                _energy.Value = _maxEnergy;

                _fullOfEnergyEvent?.Invoke();
            }                

            Debug.Log($"Восстановление энергии: {_energy.Value}");
        }
    }
}