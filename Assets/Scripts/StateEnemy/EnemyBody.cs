using Assets.Scripts.Service;
using Assets.Scripts.Service.Unit;
using Assets.Scripts.StateEnemy;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
[RequireComponent(typeof(IMoveUnit), typeof(IDamageDealer), typeof(IHealth))]
[RequireComponent (typeof(IDead))]
public class EnemyBody : MonoBehaviour, IDamagable, IUnit
{
    [SerializeField] private Transform _patrolRoute;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private BoxCollider2D _attackAria;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _radiusFOVPatrolling;
    [SerializeField] private float _radiusFOVPersecution;
    [SerializeField] private float _radiusFOVAttack;

    private string _currentStateText;
    private bool _isTargetInFOV;

    public IDamageDealer DamageDealer { get; private set; }
    public IMoveUnit Move { get; private set; }
    public ITarget Target { get; private set; }
    public IHealth Health { get; private set; }
    public IDead Dead { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Transform PatrolRoute { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Transform EyePosition { get; private set; }
    public Rigidbody2D Rigidbody {get; private set;}
    public LayerMask TargetLayer { get; private set; }

    private void Awake()
    {
        Move = GetComponent<IMoveUnit>();
        DamageDealer = GetComponent<IDamageDealer>();
        Health = GetComponent<IHealth>();
        Dead = GetComponent<IDead>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Health.Died += Dead.SetDead;
    }

    private void Start()
    {
        PatrolRoute = _patrolRoute;
        EyePosition = _eyePosition;
        TargetLayer = _targetLayer;
        _isTargetInFOV = false;
        StateMachine = new StateMachine(this, _radiusFOVPatrolling, _radiusFOVPersecution, _radiusFOVAttack);

        EnemyStateType startState = EnemyStateType.Patrolling;
        StateMachine.Initialize(startState);
    }

    private void Update()
    {
        _currentStateText = StateMachine.CurrentState.ToString();
        StateMachine.CurrentState.Update();
    }

    private void OnDrawGizmos()
    {
        StateMachine?.CurrentState.DrawGizmos();
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
        _isTargetInFOV = false;
        Target?.UndetectedByEnemy();
        Health.Died -= Dead.SetDead;
    }

    public void SetTargetInFOV(bool found)
    {
        _isTargetInFOV = found;
    }

    public void SetTargetPlayer(ITarget target)
    {
        Target = target;
    }

    public bool GetIsTargetInFOV()
    {
        if (Target == null)
        {
            _isTargetInFOV = false;
        }
        else
        {
            if (Target.Dead.IsDead == true)
            {
                _isTargetInFOV = false;
            }
        }

        return _isTargetInFOV;
    }
}
