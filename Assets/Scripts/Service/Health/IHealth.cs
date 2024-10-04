using System;

namespace Assets.Scripts.Service
{
    public interface IHealth
    {
        public int HealthAmount { get; }
        public int MaxHealth { get; }

        public event Action isDead;

        public void GetDamage(int damage);

        public bool TryGetHealth(int healthPoints);
    }
}
