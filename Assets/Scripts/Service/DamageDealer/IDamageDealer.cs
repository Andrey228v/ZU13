using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IDamageDealer
    {
        public int Damage { get;}
        public float AttackDistance {get;}
        public Vector2 DamageDirection { get;}
        public ITypeDamage TypeDamage {get;}
        public BoxCollider2D AttackAria { get; }

        public void Attack(IDamagable damageTaker);
        public void SetDamage(int damage);
        public void SetDamageDirection(Vector2 damageDirection);
        public void SetAttackDistance(float attackDistance);
    }
}
