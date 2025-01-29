using Assets.Scripts.Skills.SkillState;
using System;

namespace Assets.Scripts.Skills
{
    public interface ISkillStateMachine
    {
        public event Action<string> ChangedState;

        public ReadyState ReadyState { get; }

        public UsingState UsingState { get; }

        public CooldownState CooldownState { get; }
    }
}
