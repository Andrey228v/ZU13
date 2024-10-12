using System;

namespace Assets.Scripts.Service
{
    public interface IHealth
    {
        public event Action Died;
        public event Action Healed;
        public event Action Damaged;

        public int Amount { get; }
        public int MaxAmount { get; }

        public void TakeDamage(int damage);

        public bool TryTakeHealing(int healthPoints);
    }
}