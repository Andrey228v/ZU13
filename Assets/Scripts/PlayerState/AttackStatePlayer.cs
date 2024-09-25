using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public class AttackStatePlayer : StatePlayer
    {
        public AttackStatePlayer(Player player) : base(player){}

        public override void Enter()
        {
            base.Enter();

            _player.Animator.SetBool(PlayerAnimations.AnimatorParameterAttack, true);
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
            base.TriggerEnter(collider);

            if (collider.TryGetComponent(out IMoveUnit body)) 
            {
                _player.SetDamageDirection(-body.MoveDirectoin);

                if(collider.TryGetComponent(out IDamageTaker target))
                {
                    target.GetDamage(_player.Damage);

                    if (collider.TryGetComponent(out ITypeDamage typeDamage))
                    {
                        typeDamage.AttackDealer(_player);
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
