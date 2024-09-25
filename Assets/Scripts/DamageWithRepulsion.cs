using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamageWithRepulsion : MonoBehaviour, ITypeDamage
{
    [SerializeField] private Vector2 _repulsionForce;

    private bool _isDamage = false;
    private Vector2 _damageDirection;
    private Rigidbody2D _rigidbody;

    private void Awake()
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

    public void AttackDealer(IDamageDealer damageDealer)
    {
        _isDamage = true;
        _damageDirection = damageDealer.DamageDirection.normalized;
    }
}
