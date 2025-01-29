using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using Assets.Scripts.Service.Health;
using Assets.Scripts.Service.Taker;
using Assets.Scripts.Service.Unit;
using Assets.Scripts.Skills;
using System;
using UnityEngine;

[RequireComponent(typeof(ITypeDamage), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput), typeof(IDamageDealer))]
[RequireComponent(typeof(IJump), typeof(IMoveUnit), typeof(HealthUnits))]
[RequireComponent(typeof(IDead), typeof(Taker))]
public class Player : MonoBehaviour, ITarget, IDamagable, IUnit, ISkillUser
{
    public event Action Use;

    public IDamageDealer DamageDealer { get; private set; }
    public IUserInput UserInput { get; private set; }
    public IJump Jump { get; private set; }
    public IMoveUnit Move { get; private set; }
    public HealthUnits Health { get; private set; }
    public IDead Dead { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public StateMachinePlayer StateMachinePlayer { get; private set; }
    public Transform UserTransform { get; private set; }

    private void Awake()
    {
        UserInput = GetComponent<IUserInput>();
        Jump = GetComponent<IJump>();
        DamageDealer = GetComponent<IDamageDealer>();
        Move = GetComponent<IMoveUnit>();
        Health = GetComponent<HealthUnits>();
        Dead = GetComponent<IDead>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        UserTransform = GetComponent<Transform>();

        Health.Died += Dead.SetDead;
    }

    private void OnDestroy()
    {
       Health.Died -= Dead.SetDead;
    }

    private void Start()
    {
        StateMachinePlayer = new StateMachinePlayer(this);
        PlayerStateType startState = PlayerStateType.Movement;
        StateMachinePlayer.Initialize(startState);
    }
    private void FixedUpdate()
    {
        StateMachinePlayer.CurrentState.FixedUpdate();
    }

    private void Update()
    {
        StateMachinePlayer.CurrentState.Update();
        UseSkill();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StateMachinePlayer.CurrentState.TriggerEnter(collision);
    }

    public void DetectedByEnemy()
    {
        Renderer.color = Color.red;
    }

    public void UndetectedByEnemy()
    {   
        if(this != null)
        {
            Renderer.color = Color.white;
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void UseSkill()
    {
        if (UserInput.RightMouseButton)
        {
            Use?.Invoke();
        }
    }
}
