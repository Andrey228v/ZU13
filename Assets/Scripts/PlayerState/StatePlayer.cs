using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public abstract class StatePlayer
    {
        protected const string AnimatorParameterSpeed = "Speed";
        protected const string AxisHorizontal = "Horizontal";

        protected Player _player;
        protected float _horizontalMove = 0f;
        protected bool _lastDirection;

        public StatePlayer(Player player)
        {
            _player = player;
        }

        public virtual void Enter() 
        {
            _lastDirection = _player.Renderer.flipX;
        }

        public virtual void Exit() { }

        public virtual void Update() 
        {

            if (Input.GetKeyDown(KeyCode.Space) && _player.OnGrounded())
            {
                _player.Rigidbody.AddForce(new Vector2(_player.JumpForceX * _player.DirectionView, _player.JumpForceY), ForceMode2D.Impulse);
            }
        }

        public virtual void FixedUpdate() 
        {
            _horizontalMove = Input.GetAxis(AxisHorizontal);

            if (_horizontalMove < 0f)
            {
                _lastDirection = true;
            }
            else if (_horizontalMove > 0f)
            {
                _lastDirection = false;
            }

            if (_lastDirection != _player.Renderer.flipX)
            {
                _player.Renderer.flipX = (_horizontalMove < 0f) ? true : false;
            }
        }

        public virtual void TriggerEnter(Collider2D collider) { }
    }
}
