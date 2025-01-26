using Assets.Scripts.Service.Health;
using Assets.Scripts.Service.Unit;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public interface ISkillUser
    {
        public Transform UserTransform { get; }

        public HealthUnits Health { get; }

        public bool TryUseSkill();
    }
}
