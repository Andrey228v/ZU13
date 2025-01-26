using UnityEngine;

namespace Assets.Scripts.Skills.SkillUI
{
    [RequireComponent(typeof(LifeStillStateMachine), typeof(ISkillUser))]
    public class SkillGizmosDraw : MonoBehaviour
    {
        private LifeStillStateMachine _skillStateMachine;
        private ISkillUser _skillUser;
        private Vector2 _position;
        private int _range;

        private void Awake()
        {
            _skillStateMachine = GetComponent<LifeStillStateMachine>();
            _skillUser = GetComponent<ISkillUser>();
        }

        private void OnEnable()
        {
            _skillStateMachine.ChangedRange += SetRange;
        }

        private void Start()
        {
            _position = _skillUser.UserTransform.position;
        }

        private void OnDisable()
        {
            _skillStateMachine.ChangedRange -= SetRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_skillUser.UserTransform.position, _range);
        }

        private void SetRange(int range)
        {
            _range = range;
        }
    }
}
