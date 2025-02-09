using Assets.Scripts.Skills.SkillState;
using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public interface ISkillStateMachine
    {
        public event Action<string> ChangedState;

        public GameObject AriaTypeSkill { get; }

        public ReadyState ReadyState { get; }

        public UsingState UsingState { get; }

        public CooldownState CooldownState { get; }
    }
}
