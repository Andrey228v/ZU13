using UnityEngine;

namespace Assets.Scripts.Skills.SkillUI
{
    [RequireComponent(typeof(LifeStillStateMachine), typeof(ISkillUser))]
    public class AriaSkillDraw : MonoBehaviour
    {
        private LifeStillStateMachine _skillStateMachine;
        private ISkillUser _skillUser;
        private Vector2 _position;

        private void Awake()
        {
            _skillStateMachine = GetComponent<LifeStillStateMachine>();
            _skillUser = GetComponent<ISkillUser>();
        }

        private void Start()
        {
            _skillStateMachine.ReadyState.ChangedRange += SetRange;
            _skillStateMachine.UsingState.ChangedRange += SetRange;
            _skillStateMachine.CooldownState.ChangedRange += SetRange;
            _skillUser.Move.ChangedPosition += SetPosition;

            _position = _skillUser.UserTransform.position;
        }

        private void OnDestroy()
        {
            _skillStateMachine.ReadyState.ChangedRange -= SetRange;
            _skillStateMachine.UsingState.ChangedRange -= SetRange;
            _skillStateMachine.CooldownState.ChangedRange -= SetRange;
            _skillUser.Move.ChangedPosition -= SetPosition;
        }

        private void SetPosition(Vector3 position)
        {
            _skillStateMachine.AriaTypeSkill.transform.position = position;
        }

        private void SetRange(int range)
        {
            int radius = range * 2;
            _skillStateMachine.AriaTypeSkill.transform.localScale = new Vector2(radius, radius);
        }
    }
}
