using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using Assets.Scripts.Service.Unit;
using UnityEngine;

[RequireComponent(typeof(ITypeDamage), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D), typeof(IUserInput), typeof(IDamageDealer))]
[RequireComponent(typeof(IJump), typeof(IMoveUnit), typeof(IHealth))]
public class Player : MonoBehaviour, ITarget, IDamagable, IUnit
{
    public IDamageDealer DamageDealer { get; private set; }
    public IUserInput UserInput { get; private set; }
    public IJump Jump { get; private set; }
    public IMoveUnit Move { get; private set; }
    public IHealth Health { get; private set; }
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
        Health = GetComponent<IHealth>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
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
            if (collision.gameObject.TryGetComponent(out HealKit healKit))
            {
                if (Health.TryGetHealth(healKit.HealthPoints))
                {
                    healKit.TakeObject();
                }
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

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
