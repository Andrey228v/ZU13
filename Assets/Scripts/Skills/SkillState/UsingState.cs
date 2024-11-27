using UnityEngine;

namespace Assets.Scripts.Skills.SkillState
{
    public class UsingState : StateSkill
    {
        private float _time = 0;
        private float _perSecond = 0;
        private float _second = 1f;
        private int _backGorundCoordZ = -1;

        public UsingState(LifeStillModel model) : base(model) {}

        public override void Enter()
        {
            base.Enter();
            _model.UI.text = "UsingState";
        }

        public override void Exit()
        {
            base.Exit();
            _time = 0;
        }

        public override void Update()
        {
            base.Update();

            if (_time <= _model.UseTime && _model.Target.Dead.IsDead == false)
            {
                _time += Time.deltaTime;
                _model.LineRenderer.SetPosition(0, new Vector3(_model.Player.transform.position.x, _model.Player.transform.position.y, _backGorundCoordZ));
                _model.LineRenderer.SetPosition(1, new Vector3(_model.Target.transform.position.x, _model.Target.transform.position.y, _backGorundCoordZ));

                if (_time >= _second)
                {
                    _model.Target.Health.TakeDamage(_model.Damage);
                    _model.Player.Health.TryTakeHealing(_model.Heal);
                    _second++;
                }
            }
            else 
            {
                _model.LineRenderer.SetPosition(0, Vector3.zero);
                _model.LineRenderer.SetPosition(1, Vector3.zero);
                _model.Player.Skill.StateMachineSkill.SelectState(SkillStateType.Cooldown);
                _second = 1;
            }
        }
    }
}
