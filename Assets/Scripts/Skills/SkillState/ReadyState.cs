using System;
using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class ReadyState : IStateSkill
    {
        public event Action<SkillStateType> ChangedState;

        private ISkillUser _user;
        private int _range;
        private int _targetLayer;
        private TargetSearcher _targetSearcher;

        public ReadyState(LifeStillTarget target, ISkillUser user, int range, int targetLayer, TargetSearcher targetSearcher)
        {
            _user = user;
            _range = range;
            _targetLayer = targetLayer;
            _targetSearcher = targetSearcher;
        }

        public void Enter(){}

        public void Update()
        {
            if (_user.TryUseSkill() && _targetSearcher.TryFindTarget(_user, _range, _targetLayer))
            {
                ChangedState?.Invoke(SkillStateType.Using);
            }
        }
    }
}
