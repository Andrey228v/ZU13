using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class TargetSearcher 
    {
        private LifeStillTarget _lifeStillTarget;

        public TargetSearcher(LifeStillTarget target)
        {
            _lifeStillTarget = target;
        }

        public bool TryFindTarget(ISkillUser user, int range, int targetLayer)
        {
            bool isFind = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(user.UserTransform.position, range, targetLayer);

            if (colliders.Length > 0)
            {
                isFind = true;

                if (colliders.Length > 1)
                {
                    _lifeStillTarget.SetTarget(FindTarget(colliders, user).GetComponent<EnemyBody>());
                }
                else
                {
                    _lifeStillTarget.SetTarget(colliders[0].GetComponent<EnemyBody>());
                }
            }
            else
            {
                _lifeStillTarget.SetTarget(null);
            }

            return isFind;
        }

        private Collider2D FindTarget(Collider2D[] colliders, ISkillUser user)
        {
            Collider2D minDistanceCollider = colliders[0];

            for (int i = 1; i < colliders.Length; i++)
            {
                if ((colliders[i].transform.position - user.UserTransform.position).sqrMagnitude < (minDistanceCollider.transform.position - user.UserTransform.transform.position).sqrMagnitude)
                {
                    minDistanceCollider = colliders[i];
                }
            }

            return minDistanceCollider;
        }
    }
}
