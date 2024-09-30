using Assets.Scripts.Service;
using Assets.Scripts.StateEnemy;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
[RequireComponent(typeof(IMoveUnit), typeof(IDamageDealer))]
public class EnemyBody : MonoBehaviour, IDamageTaker
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Transform _patrolRoute;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private BoxCollider2D _attackAria;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _radiusFOVPatrolling;
    [SerializeField] private float _radiusFOVPersecution;
    [SerializeField] private float _radiusFOVAttack;

    private string _currentStateText;
    private Vector2 _velocity;

    public IDamageDealer DamageDealer { get; private set; }
    public IMoveUnit Move { get; private set; }
    public ITarget Target { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Transform PatrolRoute { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Transform EyePosition { get; private set; }
    public BoxCollider2D AttackAria { get; private set; }
    public Rigidbody2D Rigidbody {get; private set;}
    public LayerMask TargetLayer { get; private set; }
    public bool IsTargetInFOV { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Move = GetComponent<IMoveUnit>();
        DamageDealer = GetComponent<IDamageDealer>();
    }

    private void Start()
    {
        PatrolRoute = _patrolRoute;
        EyePosition = _eyePosition;
        TargetLayer = _targetLayer;
        IsTargetInFOV = false;
        AttackAria = _attackAria;

        StateMachine = new StateMachine(this, _radiusFOVPatrolling, _radiusFOVPersecution, _radiusFOVAttack);

        EnemyStateType startState = EnemyStateType.Patrolling;
        StateMachine.Initialize(startState);
    }

    private void Update()
    {
        _velocity = Rigidbody.velocity;
        _currentStateText = StateMachine.CurrentState.ToString();
        StateMachine.CurrentState.Update();
    }

    private void OnDrawGizmos()
    {
        StateMachine.CurrentState.DrawGizmos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StateMachine.CurrentState.TriggerEnter(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StateMachine.CurrentState.TriggerEnter(collision);
    }

    private void OnDestroy()
    {
        IsTargetInFOV = false;
        Target.UndetectedByEnemy();
    }

    public void SetTargetInFOV(bool found)
    {
        IsTargetInFOV = found;
    }

    public void SetTargetPlayer(ITarget target)
    {
        Target = target;
    }

    public void GetDamage(IDamageDealer damageDealer) 
    {
        _health -= damageDealer.Damage;

        if (_health < 0)
        {
            _health = 0;
        }

        if (_health == 0)
        {
            Destroy(gameObject);
        }
    }
}
