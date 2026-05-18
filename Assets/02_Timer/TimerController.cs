using UnityEngine;

namespace Timer
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private float _startime = 12;

        private TimerTestingInput _inputUser;
        private Timer _timer;        

        public void Initialize(TimerTestingInput inputUser, Timer timer)
        {
            _inputUser = inputUser;
            _timer = timer;

            _inputUser.StartPressed += OnStartTime;
            _inputUser.StopPressed += OnStopTime;
            _inputUser.ResetPressed += OnResetTime;

            OnResetTime();
        }

        private void OnDestroy()
        {
            _inputUser.StartPressed -= OnStartTime;
            _inputUser.StopPressed -= OnStopTime;
            _inputUser.ResetPressed -= OnResetTime;
        }

        public void Update()
        {
            _timer.Update(Time.deltaTime);
        }

        private void OnStartTime()
        {
            _timer.Start();
        }

        private void OnStopTime()
        {
            _timer.Stop();
        }

        private void OnResetTime()
        {
            _timer.ResetTime(_startime);
        }
    }
}