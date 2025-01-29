using System;

namespace Assets.Scripts.Skills.SkillState
{
    public interface  IStateSkill
    {
        public event Action<SkillStateType> ChangedState;

        public void Enter();

        public void Exit();

        public void Update();
    }
}
