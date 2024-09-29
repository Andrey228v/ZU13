
using UnityEngine;

namespace Assets.Scripts.Service.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveWithKinematic : MonoBehaviour, IMoveUnit
    {
        [SerializeField] private float _maxSpeed;
        public float MaxSpeed { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            MaxSpeed = _maxSpeed;
        }

        public void SetSpeed(float speed)
        {
            MaxSpeed = speed;
        }

        public void SetMoveDirection(Vector2 moveDirectoin)
        {
            MoveDirection = moveDirectoin;
        }

        public void Action(Vector2 direction)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, direction, _maxSpeed * Time.deltaTime);
        }
    }
}
