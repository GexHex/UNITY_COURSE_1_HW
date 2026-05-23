using System;

namespace Timer
{
    public class ReactiveTime
    {
        public Action<float, float> Changed;

        private float _value;

        public ReactiveTime() => _value = default(float);

        public ReactiveTime(float value) => _value = value;

        public float Value
        {
            get => _value;

            set
            {
                if (_value == value)
                    return;

                float oldValue = _value;

                _value = value;

                Changed?.Invoke(oldValue, _value);
            }
        }
    }
}