using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public class AttackStatePlayer : StatePlayer
    {
        private bool _isAttack = false;

        public AttackStatePlayer(Player player) : base(player){}

        public override void Enter()
        {
            base.Enter();

            _player.Animator.SetBool(PlayerAnimations.AnimatorParameterAttack, true);
            _isAttack = false;
        }

        public override void Exit()
        {
            base.Exit();

            _player.Animator.SetBool(PlayerAnimations.AnimatorParameterAttack, false);
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
            if (_isAttack == false)
            {
                base.TriggerEnter(collider);

                if (collider.TryGetComponent(out IMoveUnit body))
                {
                    _player.SetDamageDirection(-body.MoveDirectoin);

                    if (collider.TryGetComponent(out IDamageTaker target))
                    {
                        _player.Attack(target);
                        target.GetDamage(_player);
                        _isAttack = true;
                    }
                }
            }
        }

        public bool IsAnimationPlaying()
        {
            AnimatorStateInfo animationStateInfo = _player.Animator.GetCurrentAnimatorStateInfo(0);

            return animationStateInfo.IsName(PlayerAnimations.AnimationNameAttack) && animationStateInfo.normalizedTime < 1.0f;
        }
    }
}
