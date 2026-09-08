using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.Statistics
{
    public class StatsPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _value;
        private readonly string _prefix;
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public StatsPresenter(
            IReadOnlyVariable<int> value,
            string prefix,
            IconTextView view)
        {
            _value = value;
            _prefix = prefix;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_value.Value);

            _disposable = _value.Subscribe(OnChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText($"{_prefix}{value}");
    }
}
