using System;

namespace Assets.Scripts.Skills.SkillState
{
    public class ReadyState : IStateSkill
    {
        private ISkillUser _user;
        private int _range;
        private int _targetLayer;
        private TargetSearcher _targetSearcher;

        public ReadyState(ISkillUser user, int range, int targetLayer, TargetSearcher targetSearcher)
        {
            _user = user;
            _range = range;
            _targetLayer = targetLayer;
            _targetSearcher = targetSearcher;
        }

        public event Action<SkillStateType> ChangedState;
        public event Action<int> ChangedRange;
        public event Action UsedSkill;

        public void Enter()
        {
            _user.Use += TryUseSkill;
            ChangedRange?.Invoke(_range);
        }

        public void Exit()
        {
            _user.Use -= TryUseSkill;
        }

        public void Update(){}

        private void TryUseSkill()
        {
            ChangedState?.Invoke(SkillStateType.Using);
        }
    }
}
