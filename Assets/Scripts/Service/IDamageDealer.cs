using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IDamageDealer
    {
        public int Damage { get;}
        public float AttackDistance {get;}
        public float AttackSpeed {get;}
        public Vector2 DamageDirection { get;}
        
        public void SetDamage(int damage);
        public void SetDamageDirection(Vector2 damageDirection);
        public void SetAttackDistance(float attackDistance);
        public void SetAttackSpeed(float attackSpeed);
    }
}
