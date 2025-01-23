using Assets.Scripts.Skills.SkillState;
using System;

namespace Assets.Scripts.Skills
{
    public interface ISkillView
    {
        public event Action<string> ChangedState;

        public void ChangeStateEvent();

        public void SelectState(SkillStateType stateType);
    }
}
