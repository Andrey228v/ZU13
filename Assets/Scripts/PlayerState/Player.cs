using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(DamageWithRepulsion), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput))]
public class Player : MonoBehaviour, IDamageDealer, IHealthTaker, IDamageTaker, ITarget, IMoveUnit, ITaker
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _jumpForceX;
    [SerializeField] private BoxCollider2D _attackAria;

    private string CurrentStateText;

    public IUserInput UserInput { get; private set; }
    public ITypeDamage TypeDamage { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public StateMachinePlayer StateMachinePlayer { get; private set; }
    public float JumpForceY { get; private set; }
    public float JumpForceX { get; private set; }
    public bool OnGroundedTest {  get; private set; }
    public Vector2 DirectionView {  get; private set; }
    public Vector2 CurrentSpeed { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float AttackDistance { get; private set; }
    public Vector2 DamageDirection { get; private set; }
    public Vector2 MoveDirectoin { get; private set; }

    private void Awake()
    {
        UserInput = GetComponent<IUserInput>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        TypeDamage = GetComponent<ITypeDamage>();

        Damage = _damage;
    }

    private void Start()
    {
        StateMachinePlayer = new StateMachinePlayer(this);
        PlayerStateType startState = PlayerStateType.Movement;
        StateMachinePlayer.Initialize(startState);
    }

    private void Update()
    {
        StateMachinePlayer.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        JumpForceY = _jumpForceY;
        JumpForceX = _jumpForceX;
        DirectionView = (Renderer.flipX) ? Constants.RightMoveDirection : Constants.LeftMoveDirection;
        CurrentSpeed = Rigidbody.velocity;
        Speed = _maxSpeed;
        CurrentStateText = StateMachinePlayer.CurrentState.ToString();

        OnGroundedTest = OnGrounded();

        StateMachinePlayer.CurrentState.FixedUpdate();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ITakerObject item))
        {
            if (collision.gameObject.TryGetComponent(out HealKit healKit))
            {
                TakeHealth(healKit);
            }
            else if(collision.gameObject.TryGetComponent(out Coin coin))
            {
                coin.TakeObject();
            }
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

    public void TakeHealth(HealKit healthKit) 
    {
        if(_health < _maxHealth)
        {
            _health += healthKit.HealthPoints;

            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }

            healthKit.TakeObject();
        }
    }

    public void Attack(IDamageTaker damageTaker) 
    {
        TypeDamage.HitDamageType(this, damageTaker);
    }

    public void GetDamage(IDamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;

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
