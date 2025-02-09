using UnityEngine;

namespace Assets.Scripts.Skills.SkillUI
{
    [RequireComponent(typeof(LifeStillStateMachine), typeof(ISkillUser))]
    public class AriaSkillDraw : MonoBehaviour
    {
        private GameObject _ariaTypeSkill;
        private int _coefficientRangeToRadius = 2;

        public void SetAria(GameObject aria)
        {
            _ariaTypeSkill = aria;
        }

        public void SetPosition(Vector3 position)
        {
            _ariaTypeSkill.transform.position = position;
        }

        public void SetRange(int range)
        {
            int radius = range * _coefficientRangeToRadius;
            _ariaTypeSkill.transform.localScale = new Vector2(radius, radius);
        }
    }
}
