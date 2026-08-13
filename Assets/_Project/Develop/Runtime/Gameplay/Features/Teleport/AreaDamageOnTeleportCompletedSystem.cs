using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class AreaDamageOnTeleportCompletedSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _completedTeleportEvent;
        private ReactiveEvent _runAreaDamageEvent;

        private IDisposable _disposable;

        public void OnDispose()
        {
            _disposable.Dispose();
        }

        public void OnInit(Entity entity)
        {
            _completedTeleportEvent = entity.CompletedTeleportEvent;
            _runAreaDamageEvent = entity.RunAreaDamageEvent;

            _disposable = _completedTeleportEvent.Subscribe(RunAreaDamage);
        }

        private void RunAreaDamage() => _runAreaDamageEvent.Invoke();
    }
}