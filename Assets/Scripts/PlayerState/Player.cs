using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(DamageWithRepulsion), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput), typeof(IDamageDealer))]
[RequireComponent(typeof(IJump), typeof(IMoveUnit))]
public class Player : MonoBehaviour, IHealthTaker, IDamageTaker, ITarget
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private BoxCollider2D _attackAria;

    public IDamageDealer DamageDealer { get; private set; }
    public IUserInput UserInput { get; private set; }
    public IJump Jump { get; private set; }
    public IMoveUnit Move { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public StateMachinePlayer StateMachinePlayer { get; private set; }
    public BoxCollider2D AttackAria { get; private set; }
    public Vector2 DirectionView {  get; private set; }

    private void Awake()
    {
        UserInput = GetComponent<IUserInput>();
        Jump = GetComponent<IJump>();
        DamageDealer = GetComponent<IDamageDealer>();
        Move = GetComponent<IMoveUnit>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        AttackAria = _attackAria;
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
        DirectionView = (Renderer.flipX) ? Constants.RightMoveDirection : Constants.LeftMoveDirection;
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

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
