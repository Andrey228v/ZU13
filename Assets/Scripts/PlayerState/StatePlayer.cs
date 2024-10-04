using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public abstract class StatePlayer
    {
        protected Player Player;
        protected float HorizontalMove = 0f;
        protected bool LastDirection;

        public StatePlayer(Player player)
        {
            Player = player;
        }

        public virtual void Enter() 
        {
            LastDirection = Player.Renderer.flipX;
            Player.Move.SetMoveDirection(Player.Renderer.flipX ? Constants.LeftMoveDirection : Constants.RightMoveDirection);
        }

        public virtual void Exit() { }

        public virtual void Update() 
        {
            HorizontalMove = Player.UserInput.HorizontalMove;

            if (Player.UserInput.Jump && OnGrounded())
            {
                Player.Jump.Action(Player.Move.MoveDirection);
            }

            Debug.DrawRay(Player.transform.position, Player.Move.MoveDirection.normalized, Color.black);
        }

        public virtual void FixedUpdate() 
        {
            if (HorizontalMove < 0f)
            {
                LastDirection = true;
                Player.Move.SetMoveDirection(Constants.LeftMoveDirection);
            }
            else if (HorizontalMove > 0f)
            {
                LastDirection = false;
                Player.Move.SetMoveDirection(Constants.RightMoveDirection);
            }

            if (LastDirection != Player.Renderer.flipX)
            {
                Player.Renderer.flipX = (HorizontalMove < 0f) ? true : false;
                Player.DamageDealer.AttackAria.offset = new Vector2(Player.DamageDealer.AttackAria.offset.x * Constants.Flip, Player.DamageDealer.AttackAria.offset.y);
            }

            Player.Animator.SetFloat(PlayerAnimations.AnimatorParameterSpeed, Mathf.Abs(HorizontalMove));
            Player.Move.Action(new Vector2(HorizontalMove, 0));
        }

        public virtual void TriggerEnter(Collider2D collider) { }

        public bool OnGrounded()
        {
            RaycastHit2D hit = Physics2D.BoxCast(Player.PlayerCollider.bounds.center,
                Player.PlayerCollider.bounds.extents, 0f, Vector2.down,
                Player.PlayerCollider.bounds.extents.y);

            return (hit.collider != null) ? true : false;
        }
    }
}
