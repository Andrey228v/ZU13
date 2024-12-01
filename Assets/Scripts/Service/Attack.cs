using Assets.Scripts.Service.Unit;
using UnityEngine;
using Assets.Scripts.Service.Health;

namespace Assets.Scripts.Service
{
    public class Attack
    {
        public void SetAttack(IUnit unit, Collider2D collider)
        {
            unit.DamageDealer.SetDamageDirection(unit.Move.MoveDirection);

            if (collider.TryGetComponent(out HealthUnits targetHealth))
            {
               targetHealth.TakeDamage(unit.DamageDealer.Damage);
            }

            if (collider.TryGetComponent(out IDamagable target))
            {
                unit.DamageDealer.Attack(target);
            }
        }
    }
}
