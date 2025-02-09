using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class Timer : MonoBehaviour
    {
        public event Action StartingTimer;
        public event Action StopingTimer;
        public event Action Tick;

        private float _perSecond = 0;
        private float _second = 1f;
        private float _stopTime = 0;
        private float _period = 1f;

        public event Action<string> ChangingTime;
        public event Action TickingPeriod;

        public float Time {get; private set;}
        public bool IsWorking { get; private set; }
        public float TimeWithPeriod { get; private set; }

        private void Start()
        {
            Time = 0;
            IsWorking = false;
        }

        private void Update()
        {
            if (IsWorking)
            {
                if (Time <= _stopTime)
                {
                    Time += UnityEngine.Time.deltaTime;
                    ChangingTime?.Invoke(GetLeftTime());
                }
                else
                {
                    StopingTimer?.Invoke();
                    IsWorking = false;
                }

                if(Time >= TimeWithPeriod)
                {
                    TickingPeriod?.Invoke();
                    TimeWithPeriod += _period;
                }
            }
        }

        public void StartTimer()
        {
            StartingTimer?.Invoke();
            IsWorking = true;
        }

        public void SetStopTime(float time)
        {
            _stopTime = time;
        }

        public void ResetTime()
        {
            Time = 0;
        }

        public string GetLeftTime()
        {
            return (_stopTime - Time > 0) ? (_stopTime - Time).ToString("F1") : String.Empty;
        }
    }
}
