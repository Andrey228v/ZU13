using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(CapsuleCollider2D))]
public class PositionChanger : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string AnimatorParameterSpeed = "Speed";

    [Range(0, 10)][SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _platformLayerMask;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private CapsuleCollider2D _boxCollider;
    private float _horizontalMove = 0f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis(AxisHorizontal) * _speed;
        _animator.SetFloat(AnimatorParameterSpeed, Mathf.Abs(_horizontalMove));

        _renderer.flipX = (_horizontalMove < 0f) ? true : false;

        if (Input.GetKeyDown(KeyCode.Space) && OnGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove, _rigidbody.velocity.y);
        _rigidbody.velocity = targetVelocity;
    }

    private bool OnGrounded()
    {
        bool isHitGround;

        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center,
            _boxCollider.bounds.extents, 0f, Vector2.down,
            _boxCollider.bounds.extents.y,
            _platformLayerMask);

        Color rayColor;

        isHitGround = (hit.collider != null)? true : false;

        return isHitGround;
    }
}
