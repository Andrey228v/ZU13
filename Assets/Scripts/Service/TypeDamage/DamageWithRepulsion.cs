using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamageWithRepulsion : MonoBehaviour, ITypeDamage
{
    [SerializeField] private Vector2 _repulsionForce;

    public void HitDamageType(IDamageDealer damageDealer, IDamagable damageTaker)
    {
        Vector2 damageDirection = damageDealer.DamageDirection.normalized;
        damageTaker.Rigidbody.AddForce(_repulsionForce * damageDirection, ForceMode2D.Impulse);
    }
}
