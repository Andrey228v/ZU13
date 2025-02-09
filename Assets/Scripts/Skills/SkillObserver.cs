using Assets.Scripts.Skills.SkillUI;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(ISkillStateMachine), typeof(SkillUIText), typeof(AriaSkillDraw))]
    [RequireComponent(typeof(Timer))]
    public class SkillObserver: MonoBehaviour
    {
        private ISkillStateMachine _skillStateMachine;
        private SkillUIText _skillUIText;
        private AriaSkillDraw _ariaSkillDraw;
        private ISkillUser _skillUser;
        private Timer _timer;

        private void Awake()
        {
            _skillStateMachine = GetComponent<ISkillStateMachine>();
            _skillUIText = GetComponent<SkillUIText>();
            _ariaSkillDraw = GetComponent<AriaSkillDraw>();
            _skillUser = GetComponent<ISkillUser>();
            _timer = GetComponent<Timer>();
        }

        private void Start()
        {
            _ariaSkillDraw.SetAria(_skillStateMachine.AriaTypeSkill);

            _skillStateMachine.ChangedState += _skillUIText.SetStateText;
            _timer.ChangingTime += _skillUIText.SetTimeText;

            _skillStateMachine.ReadyState.ChangedRange += _ariaSkillDraw.SetRange;
            _skillStateMachine.UsingState.ChangedRange += _ariaSkillDraw.SetRange;
            _skillStateMachine.CooldownState.ChangedRange += _ariaSkillDraw.SetRange;
            _skillUser.Move.ChangedPosition += _ariaSkillDraw.SetPosition;
        }

        private void OnDestroy()
        {
            _skillStateMachine.ChangedState -= _skillUIText.SetStateText;
            _timer.ChangingTime -= _skillUIText.SetTimeText;

            _skillStateMachine.ReadyState.ChangedRange -= _ariaSkillDraw.SetRange;
            _skillStateMachine.UsingState.ChangedRange -= _ariaSkillDraw.SetRange;
            _skillStateMachine.CooldownState.ChangedRange -= _ariaSkillDraw.SetRange;
            _skillUser.Move.ChangedPosition -= _ariaSkillDraw.SetPosition;
        }
    }
}
