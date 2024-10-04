using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public class AttackStatePlayer : StatePlayer
    {
        private bool _isAttack = false;
        private Attack _attack = new Attack();

        public AttackStatePlayer(Player player) : base(player){}

        public override void Enter()
        {
            base.Enter();

            Player.Animator.SetBool(PlayerAnimations.AnimatorParameterAttack, true);
            _isAttack = false;
        }

        public override void Exit()
        {
            base.Exit();

            Player.Animator.SetBool(PlayerAnimations.AnimatorParameterAttack, false);
        }

        public override void Update() 
        {
            base.Update();

            if (IsAnimationPlaying() == false)
            {
                Player.StateMachinePlayer.ChangeState(Player.StateMachinePlayer.PastState);
            }
        }

        public override void TriggerEnter(Collider2D collider)
        {
            base.TriggerEnter(collider);

            if (_isAttack == false)
            {
                //Player.DamageDealer.SetDamageDirection(-Player.Move.MoveDirection);

                //if (collider.TryGetComponent(out IHealth targetHealth))
                //{
                //    targetHealth.GetDamage(Player.DamageDealer.Damage);
                //    _isAttack = true;
                //}

                //if(collider.TryGetComponent(out IDamagable target))
                //{
                //    Player.DamageDealer.Attack(target);
                //}
                _attack.SetAttack(Player, collider);

                //_isAttack = true;
            }
        }

        public bool IsAnimationPlaying()
        {
            AnimatorStateInfo animationStateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);

            return animationStateInfo.IsName(PlayerAnimations.AnimationNameAttack) && animationStateInfo.normalizedTime < 1.0f;
        }
    }
}
