using UnityEngine;

namespace Assets.Scripts.Service.Jump
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpWithForce: MonoBehaviour, IJump
    {
        [SerializeField] private float _forceY;
        [SerializeField] private float _forceX;

        public Rigidbody2D Rigidbody { get; private set; }
        public float ForceY { get; private set; }
        public float ForceX { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ForceY = _forceY;
            ForceX = _forceX;
        }
        
        public void Action(Vector2 direction)
        {
            Rigidbody.AddForce(new Vector2(_forceX * -direction.x, _forceY), ForceMode2D.Impulse);
        }
    }
}
