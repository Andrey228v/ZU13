using System;
using Assets.Scripts.Service;
using Assets.Scripts.StateEnemy;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class EnemyBody : MonoBehaviour, IDamageDealer, IDamageTaker, IMoveUnit
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private Transform _patrolRoute;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private BoxCollider2D _attackAria;
    [SerializeField] private float _radiusFOVPatrolling;
    [SerializeField] private float _radiusFOVPersecution;
    [SerializeField] private float _radiusFOVAttack;

    private string _currentStateText;
    private Vector2 _velocity;

    public ITypeDamage TypeDamage { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Transform PatrolRoute { get; private set; }
    public float Speed { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public PatrollingState PatrollingState { get; private set; }
    public PersecutionState PersecutionState { get; private set; }
    public AttackState AttackState { get; private set; }
    public Transform EyePosition { get; private set; }
    public float AttackDistance {  get; private set; }
    public LayerMask TargetLayer { get; private set; }
    public bool IsTargetInFOV { get; private set; }
    public ITarget Target { get; private set; }
    public BoxCollider2D AttackAria { get; private set; }
    public Vector2 MoveDirectoin { get; private set; }
    public Rigidbody2D Rigidbody {get; private set;}
    public int Damage { get; private set; }
    public Vector2 DamageDirection { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        TypeDamage = GetComponent<ITypeDamage>();

        Damage = _damage;
    }

    private void Start()
    {
        PatrolRoute = _patrolRoute;
        EyePosition = _eyePosition;
        AttackDistance = 2;
        TargetLayer = _targetLayer;
        IsTargetInFOV = false;
        AttackAria = _attackAria;

        StateMachine = new StateMachine();
        PatrollingState = new PatrollingState(this, _radiusFOVPatrolling);
        PersecutionState = new PersecutionState(this, _radiusFOVPersecution);
        AttackState = new AttackState(this, _radiusFOVAttack);

        StateMachine.Initialize(PatrollingState);
    }

    private void Update()
    {
        Speed = _speed;
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

    public void ChangeState(EnemyStateType stateType)
    {
        switch (stateType)
        {
            case EnemyStateType.Patrolling:
                StateMachine.ChangeState(PatrollingState);
                break;

            case EnemyStateType.Persecution:
                StateMachine.ChangeState(PersecutionState);
                break;

            case EnemyStateType.Attack:
                StateMachine.ChangeState(AttackState);
                break;
            default:
                Console.WriteLine("Такого состояния нет");
                break;
        } 
    }

    public void SetTargetInFOV(bool found)
    {
        IsTargetInFOV = found;
    }

    public void SetTargetPlayer(ITarget target)
    {
        Target = target;
    }

    public void SetMoveDirection(Vector2 moveDirectoin)
    {
        MoveDirectoin = moveDirectoin;
    }
   
    public void SetDamage(int damage)
    {
        Damage = damage;
    }
    
    public void SetDamageDirection(Vector2 damageDirection)
    {
        DamageDirection = damageDirection;
    }

    public void SetAttackDistance(float attackDistance) { AttackDistance = attackDistance; }

    public void Attack(IDamageTaker damageTaker) 
    {
        TypeDamage.HitDamageType(this, damageTaker);
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

    public void SetSpeed(float speed) { Speed = speed; }
}
