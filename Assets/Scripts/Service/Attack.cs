using Assets.Scripts.Service.Unit;
using UnityEngine;

namespace Assets.Scripts.Service
{
    public class Attack
    {
        public void SetAttack(IUnit unit, Collider2D collider)
        {
            Debug.Log($"collider:{collider}");

            unit.DamageDealer.SetDamageDirection(unit.Move.MoveDirection);

            if (collider.TryGetComponent(out IHealth targetHealth))
            {
                Debug.Log("TEST 2");
                targetHealth.GetDamage(unit.DamageDealer.Damage);
                //_isAttack = true;
            }

            if (collider.TryGetComponent(out IDamagable target))
            {
                Debug.Log("TEST 3");
                unit.DamageDealer.Attack(target);
            }
        }

    }
}
