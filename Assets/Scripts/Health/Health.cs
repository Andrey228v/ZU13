using System;
using UnityEngine;

namespace Assets.Scripts.Service.Health
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxAmount;
        [SerializeField] private int _amount;
        
        public event Action Died;
        public event Action Healed;
        public event Action Damaged;

        public int MaxAmount { get; private set; }

        public int Amount { get; private set; }

        private void Awake()
        {
            MaxAmount = _maxAmount;
            Amount = _amount;
        }

        private void Start()
        {
            Healed?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            _amount -= damage;

            if (_amount < 0)
            {
                _amount = 0;
            }

            if (_amount == 0)
            {
                Died?.Invoke();
            }

            Amount = _amount;
            Damaged?.Invoke();
        }

        public bool TryTakeHealing(int points)
        {
            bool isTake = false;

            if (_amount < _maxAmount)
            {
                _amount += points;
                isTake = true;

                if (_amount > _maxAmount)
                {
                    _amount = _maxAmount;
                }

                Amount = _amount;
                Healed?.Invoke();
            }

            return isTake;
        }
    }
}
