﻿using UnityEngine;

namespace Assets.Scripts.Service.DamageDealer
{
    public class MeleeDamageDealer : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _attackDistance;
        [SerializeField] private BoxCollider2D _attackAria;

        public int Damage {get; private set;}
        public float AttackDistance { get; private set; }
        public Vector2 DamageDirection { get; private set; }
        public ITypeDamage TypeDamage { get; private set; }
        public BoxCollider2D AttackAria { get; private set; }

        private void Awake()
        {
            AttackAria = _attackAria;
            Damage = _damage;
            AttackDistance = _attackDistance;

            TypeDamage = GetComponent<ITypeDamage>();
        }

        private void Update()
        {
            Damage = _damage;
            AttackDistance = _attackDistance;
        }

        public void Attack(IDamagable damageTaker)
        {
            TypeDamage.HitDamageType(this, damageTaker);
        }

        public void SetAttackDistance(float attackDistance)
        {
            AttackDistance = attackDistance;
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        public void SetDamageDirection(Vector2 damageDirection)
        {
            DamageDirection = damageDirection;
        }
    }
}
