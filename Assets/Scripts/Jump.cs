using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _platformLayerMask;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _boxCollider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool OnGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center,
            _boxCollider.bounds.extents, 0f, Vector2.down,
            _boxCollider.bounds.extents.y,
            _platformLayerMask);

        return (hit.collider != null) ? true : false;
    }
}
