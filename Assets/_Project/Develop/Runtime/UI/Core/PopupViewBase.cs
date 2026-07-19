using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _aniclicker;
        [SerializeField] private Transform _body;

        private Tween _currentAnimation;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public Tween Show()
        {
            KillCurrentAnimation();

            OnPreShow();

            //тут потом появятся анимации
            _mainGroup.alpha = 1;

            Sequence animation = DOTween.Sequence();

            animation
                .Append(_aniclicker
                    .DOFade(0.75f, 0.2f)
                    .From(0))
                .Join(_body
                    .DOScale(1, 0.5f)
                    .From(0)
                    .SetEase(Ease.OutBack));

            ModifyShowAnimation(animation);

            animation.OnComplete(OnPostShow);

            return _currentAnimation = animation.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
            KillCurrentAnimation();

            OnPreHide();

            Sequence animation = DOTween.Sequence();

            ModifyHideAnimation(animation);

            animation.OnComplete(OnPostHide);

            return _currentAnimation = animation.SetUpdate(true).Play();
        }

        protected virtual void ModifyShowAnimation(Sequence animation) { }
        protected virtual void ModifyHideAnimation(Sequence animation) { }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreShow() { }

        protected virtual void OnPostHide() { }

        protected virtual void OnPreHide() { }

        private void OnDestroy() => KillCurrentAnimation();

        private void KillCurrentAnimation()
        {
            if (_currentAnimation != null)
                _currentAnimation.Kill();
        }
    }
}
