using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class SpendEnergyOnTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _completedTeleportEvent;
        private ReactiveVariable<int> _energy;
        private ReactiveVariable<int> _teleportCost;
        private IDisposable _disposable;

        public void OnInit(Entity entity)
        {
            _completedTeleportEvent = entity.CompletedTeleportEvent;
            _energy = entity.Energy;
            _teleportCost = entity.TeleportEnergyCost;

            _disposable = _completedTeleportEvent.Subscribe(SpendEnergy);
        }

        public void OnDispose() => _disposable.Dispose();

        private void SpendEnergy()
        {
            _energy.Value = Mathf.Max(0, _energy.Value - _teleportCost.Value);

            Debug.Log($"Уменьшение энергии: {_energy.Value}");
        }
    }
}