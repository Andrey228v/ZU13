using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IMoveUnit
    {
        public float MaxSpeed { get;}
        public Vector2 MoveDirection { get;}
        public Rigidbody2D Rigidbody { get;}

        public void SetSpeed(float speed);
        public void SetMoveDirection(Vector2 moveDirectoin);
        public void Action(Vector2 direction);
    }
}
