using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public abstract class StatePlayer
    {
        protected const string AxisHorizontal = "Horizontal";

        protected Player _player;
        protected float _horizontalMove = 0f;
        protected bool _lastDirection;
        protected Vector2 _rightMoveDirection = new Vector2(-1, 0);
        protected Vector2 _leftMoveDirection = new Vector2(1, 0);

        public StatePlayer(Player player)
        {
            _player = player;
        }

        public virtual void Enter() 
        {
            _lastDirection = _player.Renderer.flipX;

            if (_player.Renderer.flipX == true)
            {
                _player.SetMoveDirection(_leftMoveDirection);
            }
            else
            {
                _player.SetMoveDirection(_rightMoveDirection);
            }

        }

        public virtual void Exit() { }

        public virtual void Update() 
        {
            _horizontalMove = Input.GetAxis(AxisHorizontal);

            if (_player.UserInput.Jump && _player.OnGrounded())
            {
                _player.Rigidbody.AddForce(new Vector2(_player.JumpForceX * _player.DirectionView, _player.JumpForceY), ForceMode2D.Impulse);
            }
        }

        public virtual void FixedUpdate() 
        {
            if (_horizontalMove < 0f)
            {
                _lastDirection = true;
                _player.SetMoveDirection(_rightMoveDirection);
            }
            else if (_horizontalMove > 0f)
            {
                _lastDirection = false;
                _player.SetMoveDirection(_leftMoveDirection);
            }

            if (_lastDirection != _player.Renderer.flipX)
            {
                _player.Renderer.flipX = (_horizontalMove < 0f) ? true : false;
            }

            _player.Animator.SetFloat(PlayerAnimations.AnimatorParameterSpeed, Mathf.Abs(_horizontalMove));
            Vector2 targetForce = new Vector2(_horizontalMove * _player.Speed, _player.Rigidbody.velocity.y);
            _player.Rigidbody.AddForce(targetForce, ForceMode2D.Impulse);
        }

        public virtual void TriggerEnter(Collider2D collider) { }
    }
}
