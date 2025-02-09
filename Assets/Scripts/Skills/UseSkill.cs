using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class UseSkill : IDisposable
    {
        private Timer _timer;
        private TargetSearcher _targetSearcher;
        private LifeStillTarget _target;
        private ISkillUser _user;
        private bool _isTick;

        public UseSkill(Timer timer, TargetSearcher targetSearcher)
        {
            _timer = timer;
            _targetSearcher = targetSearcher;
            _timer.TickingPeriod += ActivateTickEffect;
        }

        public void Dispose()
        {
            _timer.TickingPeriod -= ActivateTickEffect;
        }

        public void Use(LifeStillTarget target, ISkillUser user, int range, LayerMask targetLayer, int damage, LifeStillView view)
        {
                if (_targetSearcher.TryFindTarget(user, range, targetLayer))
                {
                    view.DrawLineFromUserToTarget(user.UserTransform, target.Target.transform);

                    if (_isTick)
                    {
                        int damageTake = target.Target.Health.TakeDamage(damage);
                        user.Health.TryTakeHealing(damageTake);
                        _isTick = false;
                    }
                }
                else
                {
                    view.DeletLine();
                } 
        }

        private void ActivateTickEffect()
        {
            _isTick = true;
        }
    }
}
