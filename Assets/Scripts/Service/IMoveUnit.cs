using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IMoveUnit
    {
        public float Speed { get; }
        public Vector2 MoveDirectoin { get; }

        public void SetSpeed(float speed);

        public void SetMoveDirection(Vector2 moveDirectoin);
    }
}
