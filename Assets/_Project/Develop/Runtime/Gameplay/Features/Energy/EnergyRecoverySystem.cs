using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyRecoverySystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<int> _energy;
        private ReactiveVariable<int> _maxEnergy;
        private ReactiveEvent _fullOfEnergyEvent;

        private float _timer;
        private readonly float _recoveryInterval = 2f;
        private int _recoveryValue;

        public void OnInit(Entity entity)
        {
            _energy = entity.Energy;
            _maxEnergy = entity.MaxEnergy;
            _fullOfEnergyEvent = entity.FullOfEnergyEvent;

            _recoveryValue = Mathf.Max(1, _maxEnergy.Value / 10);
            _energy.Value = Mathf.Clamp(_energy.Value, 0, _maxEnergy.Value);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_energy.Value >= _maxEnergy.Value)
            {
                _timer = 0f;

                return;
            }

            _timer += deltaTime;

            if (_timer < _recoveryInterval)
                return;

            _timer -= _recoveryInterval;

            _energy.Value = Mathf.Min(_energy.Value + _recoveryValue, _maxEnergy.Value);

            if (_energy.Value >= _maxEnergy.Value)
            {
                _fullOfEnergyEvent?.Invoke();
            }

            Debug.Log($"Восстановление энергии: {_energy.Value}");
        }
    }
}
