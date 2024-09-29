using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IJump
    {
        public Rigidbody2D Rigidbody { get;}

        public float ForceY { get;}
        public float ForceX { get;}

        public void Action(Vector2 direction);

    }
}
