using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IDamageDealer
    {
        public int Damage { get;}
        public float AttackDistance {get;}
        public Vector2 DamageDirection { get;}
        public ITypeDamage TypeDamage {get;}

        public void Attack(IDamageTaker damageTaker);
        public void SetDamage(int damage);
        public void SetDamageDirection(Vector2 damageDirection);
        public void SetAttackDistance(float attackDistance);
    }
}
