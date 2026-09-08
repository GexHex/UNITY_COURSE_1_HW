using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class PlayerCurrentTargetView : EntityView
    {
        [SerializeField] private ParticleSystem _backlightPrefab;

        private ParticleSystem _backlight;

        private ReactiveEvent<PointerMoveArgs> _pointerMoveEvent;

        private IDisposable _pointerMoveDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _pointerMoveEvent = entity.PlayerPointerMoveEvent;

            _backlight = Instantiate(_backlightPrefab);
            _backlight.gameObject.SetActive(false);

            _pointerMoveDisposable = _pointerMoveEvent.Subscribe(OnPointerMove);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _pointerMoveDisposable.Dispose();

            Destroy(_backlight.gameObject);
        }

        private void OnPointerMove(PointerMoveArgs args)
        {
            _backlight.gameObject.SetActive(true);
            _backlight.transform.position = args.GamePosition;
        }
    }
}
