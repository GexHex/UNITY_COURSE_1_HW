using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyRecoverySystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<int> _energy;
        private ReactiveEvent _fullOfEnergyEvent;

        private float _timer;
        private float _recoveryInterval = 2f;
        private int _maxEnergy = 110;
        private int _recoveryValue;

        public void OnInit(Entity entity)
        {
            _energy = entity.Energy;
            _fullOfEnergyEvent = entity.FullOfEnergyEvent;

            _recoveryValue = _maxEnergy / 10;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_energy.Value >= _maxEnergy)
                return;

            _timer += deltaTime;

            if (_timer < _recoveryInterval)
                return;

            _timer = 0;

            _energy.Value += _recoveryValue;

            if (_energy.Value >= _maxEnergy)
            {
                _energy.Value = _maxEnergy;

                _fullOfEnergyEvent?.Invoke();
            }

            Debug.Log($"Восстановление энергии: {_energy.Value}");
        }
    }
}