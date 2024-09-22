using Assets.Scripts.PlayerState;
using UnityEngine;

[RequireComponent(typeof(TakeDamage), typeof(Animator), typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _jumpForceX;
    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private BoxCollider2D _attackAria;

    private const int _leftMoveDirection = -1;
    private const int _rightMoveDirection = 1;

    private string CurrentStateText;

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
    public TakeDamage TakeDamage {get; private set; }
    public float MaxSpeed { get; private set; }

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        TakeDamage = GetComponent<TakeDamage>();

        StateMachinePlayer = new StateMachinePlayer();
        MovementState = new MovementState(this);
        AttackStatePlayer = new AttackStatePlayer(this);

        StateMachinePlayer.Initialize(MovementState);
    }

    private void Update()
    {
        DirectionView = (Renderer.flipX)? _leftMoveDirection : _rightMoveDirection;
        CurrentSpeed = Rigidbody.velocity;
        MaxSpeed = _maxSpeed;

        CurrentStateText = StateMachinePlayer.CurrentState.ToString();

        StateMachinePlayer.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        JumpForceY = _jumpForceY;
        JumpForceX = _jumpForceX;

        OnGroundedTest = OnGrounded();

        StateMachinePlayer.CurrentState.FixedUpdate();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Get();
        }

        if (collision.gameObject.TryGetComponent(out HealKit healKit))
        {
            healKit.Get();
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
            PlayerCollider.bounds.extents.y,
            _platformLayerMask);

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
}
