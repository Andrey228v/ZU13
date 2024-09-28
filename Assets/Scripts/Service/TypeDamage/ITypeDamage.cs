using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface ITypeDamage
    {
        public void HitDamageType(IDamageDealer damageDealer, IDamageTaker damageTaker);
    }
}
