using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public abstract class StateSkill
    {
        protected EnemyBody Target;

        protected LifeStillModel _model;
        protected RaycastHit2D Hit;

        public StateSkill(LifeStillModel model) 
        {
            _model = model;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void DrawRaycast() { }

        public virtual void DrawGizmos() { }

        public bool TryFindTarget()
        {
            bool isFind = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_model.Player.transform.position, _model.Range, _model.TargetLayer);

            if (colliders.Length > 0)
            {
                isFind = true;

                if (colliders.Length > 1)
                {
                    _model.SetTarget(FindMinDistance(colliders).GetComponent<EnemyBody>());
                }
                else
                {
                    _model.SetTarget(colliders[0].GetComponent<EnemyBody>());
                }
            }
            else
            {
                _model.SetTarget(null);
            }

            return isFind;
        }

        private Collider2D FindMinDistance(Collider2D[] colliders)
        {
            Collider2D minDistanceCollider = colliders[0];

            for(int i = 1; i < colliders.Length; i++)
            {
                if (Vector3.Distance(colliders[i].transform.position, _model.Player.transform.position) < Vector3.Distance(minDistanceCollider.transform.position, _model.Player.transform.position))
                {
                    minDistanceCollider = colliders[i];
                }
            }

            return minDistanceCollider;
        }
    }
}
