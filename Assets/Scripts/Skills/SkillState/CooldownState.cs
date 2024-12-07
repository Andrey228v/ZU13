using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class CooldownState : StateSkill
    {
        private float _time = 0;

        public CooldownState(LifeStillModel model) : base(model)
        {}

        public override void Enter()
        {
            base.Enter();
            _model.UI.text = $"CooldownState {_model.Cooldown}";
        }

        public override void Exit()
        {
            base.Exit();
            _time = 0;
        }

        public override void Update()
        {
            base.Update();

            if (_time < _model.Cooldown)
            {
                _time += Time.deltaTime;
                _model.UI.text = $"CooldownState {_model.Cooldown - _time}";
            }
            else
            {
                _model.Player.Skill.SelectState(SkillStateType.Ready);
            }
        }
    }
}
