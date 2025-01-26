using System;
using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class UsingState : IStateSkill
    {
        public event Action<SkillStateType> ChangedState;
        public event Action<string> ChangingTime;

        private float _time = 0;
        private float _perSecond = 0;
        private float _second = 1f;
        private int _backGorundCoordZ = -1;

        private LifeStillTarget _target;
        private ISkillUser _user;
        private int _useTime;
        private int _range;
        private LayerMask _targetLayer;
        private int _damage;
        private TargetSearcher _targetSearcher;
        private LifeStillView _view;

        public UsingState(LifeStillTarget target, ISkillUser user, int useTime, int range, LayerMask targetLayer, int damage, TargetSearcher targetSearcher, LifeStillView view)
        {
            _target = target;
            _user = user;
            _useTime = useTime;
            _range = range;
            _targetLayer = targetLayer;
            _damage = damage;
            _targetSearcher = targetSearcher;
            _view = view;
        }

        public void Enter(){}

        public void Exit()
        {
            _time = 0;
        }

        public void Update()
        {
            if (_time <= _useTime)
            {
                _time += Time.deltaTime;
                ChangingTime?.Invoke(LeftTime());

                if (_targetSearcher.TryFindTarget(_user, _range, _targetLayer))
                {
                    _view.SetLinePosition(
                        new Vector3(_user.UserTransform.position.x, _user.UserTransform.position.y, _backGorundCoordZ), 
                        new Vector3(_target.Target.transform.position.x, _target.Target.transform.position.y, _backGorundCoordZ));

                    if (_time >= _second)
                    {
                        int damageTake = _target.Target.Health.TakeDamage(_damage);
                        _user.Health.TryTakeHealing(damageTake);
                        _second++;
                    }
                }
                else
                {
                    _view.DeletLine();
                }
            }
            else 
            {
                _view.DeletLine();
                ChangedState?.Invoke(SkillStateType.Cooldown);
                _second = 1;
            }
        }

        public string LeftTime()
        {
            return String.Concat("Use: ", (_useTime - _time).ToString("F1"));
        }
    }
}
