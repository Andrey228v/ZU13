using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PositionChanger : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string AnimatorParameterSpeed = "Speed";

    [Range(0, 10)][SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _horizontalMove = 0f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis(AxisHorizontal) * _speed;
        _animator.SetFloat(AnimatorParameterSpeed, Mathf.Abs(_horizontalMove));
        _renderer.flipX = (_horizontalMove < 0f) ? true : false;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove, _rigidbody.velocity.y);
        _rigidbody.velocity = targetVelocity;
    }
}
