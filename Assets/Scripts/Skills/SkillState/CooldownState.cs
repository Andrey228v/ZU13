using System;

namespace Assets.Scripts.Skills.SkillState
{
    public class CooldownState : IStateSkill
    {
        private ISkillUser _user;
        private float _cooldown;
        private int _range;
        private Timer _timer;

        public CooldownState(ISkillUser user, float cooldown, int range, Timer timer)
        {
            _user = user;
            _cooldown = cooldown;
            _range = range;
            _timer = timer;
        }

        public event Action<SkillStateType> ChangedState;
        public event Action<int> ChangedRange;

        public void Enter()
        {
            ChangedRange?.Invoke(_range);
            _timer.SetStopTime(_cooldown);
            _timer.StartTimer();
        }

        public void Exit()
        {
            _timer.ResetTime();
        }

        public void Update()
        {
            if (_timer.IsWorking == false)
            {
                ChangedState?.Invoke(SkillStateType.Ready);
            }
        }
    }
}
