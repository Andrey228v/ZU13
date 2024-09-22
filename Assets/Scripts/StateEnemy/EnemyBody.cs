using System;
using Assets.Scripts.StateEnemy;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class EnemyBody : MonoBehaviour
{
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
    public Player Target { get; private set; }
    public BoxCollider2D AttackAria { get; private set; }
    public Vector2 MoveDirectoin { get; private set; }
    public Rigidbody2D Rigidbody {get; private set;}

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

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
                Console.WriteLine("������ ��������� ���");
                break;
        } 
    }

    public void SetTargetInFOV(bool found)
    {
        IsTargetInFOV = found;
    }

    public void SetTargetPlayer(Player player)
    {
        Target = player;
    }

    public void SetMoveDirection(Vector2 moveDirectoin)
    {
        MoveDirectoin = moveDirectoin;
    }
}
