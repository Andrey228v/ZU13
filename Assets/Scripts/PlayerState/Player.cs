using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using Assets.Scripts.Service.Health;
using Assets.Scripts.Service.Unit;
using Assets.Scripts.Skills;
using UnityEngine;

[RequireComponent(typeof(ITypeDamage), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput), typeof(IDamageDealer))]
[RequireComponent(typeof(IJump), typeof(IMoveUnit), typeof(HealthUnits))]
[RequireComponent(typeof(IDead), typeof(ISkillView))]
public class Player : MonoBehaviour, ITarget, IDamagable, IUnit
{
    public IDamageDealer DamageDealer { get; private set; }
    public IUserInput UserInput { get; private set; }
    public IJump Jump { get; private set; }
    public IMoveUnit Move { get; private set; }
    public HealthUnits Health { get; private set; }
    public IDead Dead { get; private set; }
    public ISkillView Skill { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public StateMachinePlayer StateMachinePlayer { get; private set; }

    private void Awake()
    {
        UserInput = GetComponent<IUserInput>();
        Jump = GetComponent<IJump>();
        DamageDealer = GetComponent<IDamageDealer>();
        Move = GetComponent<IMoveUnit>();
        Health = GetComponent<HealthUnits>();
        Dead = GetComponent<IDead>();
        Skill = GetComponent<ISkillView>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();

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
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ITakerObject item))
        {
            if (item is HealKit)
            {
                HealKit healKit = collision.gameObject.GetComponent<HealKit>();

                if (Health.TryTakeHealing(healKit.HealthPoints))
                {
                    item.TakeObject();
                }
            }

            if (item is Coin)
            {
                item.TakeObject();
            }
        }
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
}
