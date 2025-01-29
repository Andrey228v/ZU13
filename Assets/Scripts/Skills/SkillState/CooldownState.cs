using System;
using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class CooldownState : IStateSkill
    {
        private float _time = 0;
        private ISkillUser _user;
        private float _cooldown;
        private int _range;

        public CooldownState(ISkillUser user, float cooldown, int range)
        {
            _user = user;
            _cooldown = cooldown;
            _range = range;
        }

        public event Action<SkillStateType> ChangedState;
        public event Action<int> ChangedRange;
        public event Action<string> ChangingTime;

        public void Enter()
        {
            ChangedRange?.Invoke(_range);
        }

        public void Exit()
        {
            _time = 0;
        }

        public void Update()
        {
            if (_time < _cooldown)
            {
                _time += Time.deltaTime;
                ChangingTime?.Invoke(LeftTime());
            }
            else
            {
                ChangedState?.Invoke(SkillStateType.Ready);
            }
        }

        public string LeftTime()
        {
            return String.Concat("Cooldown: ", (_cooldown - _time).ToString("F1"));
        }
    }
}
