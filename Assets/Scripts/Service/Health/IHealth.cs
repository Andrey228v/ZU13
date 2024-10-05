using System;

namespace Assets.Scripts.Service
{
    public interface IHealth
    {
        public event Action isDead;
        public event Action isHealed;

        public int HealthAmount { get; }
        public int MaxHealth { get; }

        public void GetDamage(int damage);

        public bool TryGetHealth(int healthPoints);
    }
}
