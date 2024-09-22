using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public class AttackStatePlayer : StatePlayer
    {
        private const string AnimatorParameterAttack = "IsAttack";
        private const string AnimationNameStay = "AttackStay";
        private const string AnimationNameRun = "AttackRun";

        public AttackStatePlayer(Player player) : base(player){}

        public override void Enter()
        {
            base.Enter();

            _player.Animator.SetBool(AnimatorParameterAttack, true);
        }

        public override void Exit()
        {
            base.Exit();

            _player.Animator.SetBool(AnimatorParameterAttack, false);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _player.Animator.SetFloat(AnimatorParameterSpeed, Mathf.Abs(_horizontalMove));
            Vector2 targetForce = new Vector2(_horizontalMove * _player.MaxSpeed, _player.Rigidbody.velocity.y);
            _player.Rigidbody.AddForce(targetForce, ForceMode2D.Impulse); 
        }

        public override void Update() 
        {
            base.Update();

            if (IsAnimationPlaying() == false)
            {
                _player.StateMachinePlayer.ChangeState(_player.StateMachinePlayer.PastState);
            }
        }

        public override void TriggerEnter(Collider2D collider)
        {
            base.TriggerEnter(collider);

            if (collider.TryGetComponent(out TakeDamage target))
            {
                if (collider.TryGetComponent(out EnemyBody body))
                {
                    target.GetDamage(-body.MoveDirectoin);
                }
            }
        }

        public bool IsAnimationPlaying()
        {
            AnimatorStateInfo animationStateInfo = _player.Animator.GetCurrentAnimatorStateInfo(0);

            return animationStateInfo.IsName("Attack") && animationStateInfo.normalizedTime < 1.0f;
        }
    }
}
