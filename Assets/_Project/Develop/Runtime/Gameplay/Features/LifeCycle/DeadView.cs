using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    [RequireComponent(typeof(Animator))]
    public class DeadView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _inAttackProcess;

        private IDisposable _isDeadChangedDisposable;
        private IDisposable _inAttackProcessChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;
            _inAttackProcess = entity.InAttackProcess;

            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
            _inAttackProcessChangedDisposable = _inAttackProcess.Subscribe(OnInAttackProcessChanged);
            UpdateIsDead();
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _isDeadChangedDisposable.Dispose();
            _inAttackProcessChangedDisposable.Dispose();
        }

        private void OnIsDeadChanged(bool oldIsDead, bool isDead) => UpdateIsDead();

        private void OnInAttackProcessChanged(bool oldInAttackProcess, bool inAttackProcess) => UpdateIsDead();

        private void UpdateIsDead() => _animator.SetBool(IsDeadKey, _isDead.Value && _inAttackProcess.Value == false);
    }
}
