using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(DamageWithRepulsion), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput))]
public class Player : MonoBehaviour, IDamageDealer, IHealthTaker, IDamageTaker, ITarget, IMoveUnit
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _jumpForceX;
    [SerializeField] private BoxCollider2D _attackAria;

    private const int _leftMoveDirection = -1;
    private const int _rightMoveDirection = 1;

    private string CurrentStateText;

    public IUserInput UserInput { get; private set; }
    public ITypeDamage DamageType { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public StateMachinePlayer StateMachinePlayer { get; private set; }
    public MovementState MovementState { get; private set; }
    public AttackStatePlayer AttackStatePlayer { get; private set; }
    public float JumpForceY { get; private set; }
    public float JumpForceX { get; private set; }
    public bool OnGroundedTest {  get; private set; }
    public int DirectionView {  get; private set; }
    public Vector2 CurrentSpeed { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float AttackDistance { get; private set; }
    public float AttackSpeed { get; private set; }
    public Vector2 DamageDirection { get; private set; }
    public Vector2 MoveDirectoin { get; private set; }

    private void Awake()
    {
        UserInput = GetComponent<IUserInput>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        DamageType = GetComponent<DamageWithRepulsion>();

        Damage = _damage;
    }

    private void Start()
    {
        StateMachinePlayer = new StateMachinePlayer();
        MovementState = new MovementState(this);
        AttackStatePlayer = new AttackStatePlayer(this);

        StateMachinePlayer.Initialize(MovementState);
    }

    private void Update()
    {
        StateMachinePlayer.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        JumpForceY = _jumpForceY;
        JumpForceX = _jumpForceX;
        DirectionView = (Renderer.flipX) ? _leftMoveDirection : _rightMoveDirection;
        CurrentSpeed = Rigidbody.velocity;
        Speed = _maxSpeed;
        CurrentStateText = StateMachinePlayer.CurrentState.ToString();

        OnGroundedTest = OnGrounded();

        StateMachinePlayer.CurrentState.FixedUpdate();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ITakerObject item)) 
        {
            item.Get(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StateMachinePlayer.CurrentState.TriggerEnter(collision);
    }

    public void DetectedByEnemy()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void UndetectedByEnemy()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public bool OnGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider.bounds.center,
            PlayerCollider.bounds.extents, 0f, Vector2.down,
            PlayerCollider.bounds.extents.y);

        return (hit.collider != null) ? true : false;
    }

    public void ChangeState(PlayerStateType stateType)
    {
        switch (stateType)
        {
            case PlayerStateType.Movement:
                StateMachinePlayer.ChangeState(MovementState);
                break;

            case PlayerStateType.Attack:
                StateMachinePlayer.ChangeState(AttackStatePlayer);
                break;

            default:
                Debug.Log("Такого состояния нет");
                break;
        }
    }

    public void SetDamage(int damage) 
    {
        Damage = damage;
    }

    public void SetDamageDirection(Vector2 damageDirection) 
    {
        DamageDirection = damageDirection;
    }

    public void SetMoveDirection(Vector2 moveDirectoin)
    {
        MoveDirectoin = moveDirectoin;
    }

    public void SetAttackDistance(float attackDistance) { }

    public void SetAttackSpeed(float attackSpeed) { }

    public void TakeHealth(HealKit healthKit) 
    {
        if(_health < _maxHealth)
        {
            Destroy(healthKit.gameObject);

            _health += healthKit.HealthPoints;

            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    public void GetDamage(int damage)
    {
        _health -= damage;

        if( _health < 0)
        {
            _health = 0;
        }

        if(_health == 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed) { Speed = speed; }

    public Vector2 GetPosition() 
    {
        return transform.position;
    }
}
