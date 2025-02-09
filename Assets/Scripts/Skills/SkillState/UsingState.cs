using System;
using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class UsingState : IStateSkill
    {
        private LifeStillTarget _target;
        private ISkillUser _user;
        private int _useTime;
        private int _range;
        private LayerMask _targetLayer;
        private int _damage;
        private TargetSearcher _targetSearcher;
        private LifeStillView _view;
        private Timer _timer;
        private UseSkill _useSkill;

        public UsingState(LifeStillTarget target, ISkillUser user, int useTime, int range, LayerMask targetLayer, int damage, TargetSearcher targetSearcher, LifeStillView view, Timer timer, UseSkill useSkill)
        {
            _target = target;
            _user = user;
            _useTime = useTime;
            _range = range;
            _targetLayer = targetLayer;
            _damage = damage;
            _targetSearcher = targetSearcher;
            _view = view;
            _timer = timer;
            _useSkill = useSkill;
        }

        public event Action<SkillStateType> ChangedState;
        public event Action<int> ChangedRange;

        public void Enter()
        {
            ChangedRange?.Invoke(_range);
            _timer.SetStopTime(_useTime);
            _timer.StartTimer();
        }

        public void Exit()
        {
            _view.DeletLine();
            _timer.ResetTime();
        }

        public void Update()
        {
            if (_timer.IsWorking)
            {
                _useSkill.Use(_target, _user, _range, _targetLayer, _damage, _view);
            }
            else
            {
                ChangedState?.Invoke(SkillStateType.Cooldown);
            }
        }
    }
}
