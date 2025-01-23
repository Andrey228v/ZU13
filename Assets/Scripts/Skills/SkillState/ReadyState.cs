using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class ReadyState : StateSkill
    {
        public ReadyState(LifeStillModel model) : base(model) { }
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (_model.Player.UserInput.RightMouseButton && TryFindTarget())
            {
                _model.Player.Skill.SelectState(SkillStateType.Using);
            }
        }

        public override string TextUI()
        {
            return "ReadyState";
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();

            Gizmos.DrawWireSphere(_model.Player.transform.position, _model.Range);
        }
    }
}
