using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class StatsManagementTimerSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private float _timer;
        private float _maxTime = 2;
        private bool _isTimerStarted;

        private IDisposable _runTeleportDisposable;
        private IDisposable _fullOfEnegyEventDisposable;

        private ReactiveEvent _runTeleportEvent;
        private ReactiveEvent _statsTimerEvent;
        private ReactiveEvent _fullOfEnegyEvent;

        public void OnInit(Entity entity)
        {
            _runTeleportEvent = entity.RunTeleportEvent;
            _statsTimerEvent = entity.StatsTimerEvent;
            _fullOfEnegyEvent = entity.FullOfEnergyEvent;

            _runTeleportDisposable = _runTeleportEvent.Subscribe(StartTimer);
            _fullOfEnegyEventDisposable = _fullOfEnegyEvent.Subscribe(StopTimer);
        }

        public void OnDispose()
        {
            _runTeleportDisposable.Dispose();
            _fullOfEnegyEventDisposable.Dispose();
        }

        private void StartTimer()
        {
            _isTimerStarted = true;
        }

        private void StopTimer()
        {
            _isTimerStarted = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isTimerStarted == true)
            {
                _timer += deltaTime;               

                if (_timer >= _maxTime)
                {
                    _statsTimerEvent.Invoke();
                    _timer = 0;
                }
            }
        }
    }
}
