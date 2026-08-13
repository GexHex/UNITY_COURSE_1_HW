using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class SpendEnergyOnTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _runTeleportEvent;
        private ReactiveVariable<int> _energy;

        private IDisposable _disposable;

        private int _teleportCost = 10;
        private int _minEnergy = 0;

        public void OnInit(Entity entity)
        {
            _runTeleportEvent = entity.RunTeleportEvent;
            _energy = entity.Energy;

            _disposable = _runTeleportEvent.Subscribe(SpendEnergy);
        }

        public void OnDispose()
        {
            _disposable.Dispose();
        }

        private void SpendEnergy()
        {
            _energy.Value -= _teleportCost;

            if (_energy.Value < _minEnergy)
                _energy.Value = _minEnergy;

            Debug.Log($"Уменьшение энергии: {_energy.Value}");
        }
    }
}