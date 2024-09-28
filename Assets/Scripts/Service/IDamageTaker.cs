using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface IDamageTaker
    {
        public Rigidbody2D Rigidbody { get; }

        public void GetDamage(IDamageDealer damageDealer);
    }
}
