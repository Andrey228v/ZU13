using Assets.Scripts.Service;
using Assets.Scripts.Service.Health;
using Assets.Scripts.Service.Unit;
using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public interface ISkillUser
    {
        public event Action Use;

        public Transform UserTransform { get; }

        public HealthUnits Health { get; }

        public IMoveUnit Move { get; }

        public void UseSkill();
    }
}
