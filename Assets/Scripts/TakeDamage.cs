using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TakeDamage : MonoBehaviour
{
    [SerializeField] Vector2 _repulsionForce;

    private bool _isDamage = false;
    private Vector2 _damageDirection;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isDamage)
        {
            _rigidbody.AddForce(_repulsionForce * _damageDirection, ForceMode2D.Impulse);
            _isDamage = false;
        }
    }

    public void GetDamage(Vector2 damageDirection)
    {
        _isDamage = true;
        _damageDirection = damageDirection.normalized;
    }
}
