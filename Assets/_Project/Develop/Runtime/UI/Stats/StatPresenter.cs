using Assets._Project.Develop.Runtime.Configs.Meta.Stats;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.Stats
{
    public class StatPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _stat;
        private readonly StatsType _type;
        private readonly StatsIconsConfig _iconsConfig;
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public StatPresenter(
            IReadOnlyVariable<int> stat,
            StatsType type,
            StatsIconsConfig iconsConfig,
            IconTextView view)
        {
            _stat = stat;
            _type = type;
            _iconsConfig = iconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_stat.Value);
            _view.SetIcon(_iconsConfig.GetSpriteFor(_type));

            _disposable = _stat.Subscribe(OnStatChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnStatChanged(int oldValue, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}