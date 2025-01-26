using System;
using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class CooldownState : IStateSkill
    {
        public event Action<SkillStateType> ChangedState;
        public event Action<string> ChangingTime;

        private float _time = 0;
        private ISkillUser _user;
        private float _cooldown;

        public CooldownState(LifeStillTarget model, ISkillUser user, float cooldown)
        {
            _user = user;
            _cooldown = cooldown;
        }

        public void Enter(){}

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
