using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    [RequireComponent(typeof(Animator))]
    public class WalkingView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsWalking");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isMoving;
        private ReactiveVariable<bool> _inAttackProcess;

        private IDisposable _isMovingChangedDisposable;
        private IDisposable _inAttackProcessChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isMoving = entity.IsMoving;
            _inAttackProcess = entity.InAttackProcess;

            _isMovingChangedDisposable = _isMoving.Subscribe(OnIsMovingChanged);
            _inAttackProcessChangedDisposable = _inAttackProcess.Subscribe(OnInAttackProcessChanged);
            UpdateIsMoving();
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _isMovingChangedDisposable.Dispose();
            _inAttackProcessChangedDisposable.Dispose();
        }

        private void OnIsMovingChanged(bool oldIsMoving, bool isMoving) => UpdateIsMoving();

        private void UpdateIsMoving() => _animator.SetBool(IsMovingKey, _isMoving.Value && _inAttackProcess.Value == false);

        private void OnInAttackProcessChanged(bool oldInAttackProcess, bool inAttackProcess) => UpdateIsMoving();
    }
}
