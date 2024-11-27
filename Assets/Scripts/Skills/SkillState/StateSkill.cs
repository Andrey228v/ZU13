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
    }
}
