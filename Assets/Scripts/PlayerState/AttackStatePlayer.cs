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
            if (_isAttack == false)
            {
                base.TriggerEnter(collider);

                if (collider.TryGetComponent(out IMoveUnit body))
                {
                    Player.DamageDealer.SetDamageDirection(-Player.Move.MoveDirection);

                    if (collider.TryGetComponent(out IDamageTaker target))
                    {
                        Debug.Log("Test");

                        Player.DamageDealer.Attack(target);
                        target.GetDamage(Player.DamageDealer);
                        _isAttack = true;
                    }
                }
            }
        }

        public bool IsAnimationPlaying()
        {
            AnimatorStateInfo animationStateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);

            return animationStateInfo.IsName(PlayerAnimations.AnimationNameAttack) && animationStateInfo.normalizedTime < 1.0f;
        }
    }
}
