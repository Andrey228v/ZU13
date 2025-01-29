using Assets.Scripts.Skills.SkillState;
using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(ISkillUser), typeof(LifeStillView))]
    public class LifeStillStateMachine : MonoBehaviour, ISkillStateMachine
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private int _useTime;
        [SerializeField] private int _cooldown;
        [SerializeField] private int _damage;
        [SerializeField] private int _rangeReadyState;
        [SerializeField] private int _rangeUsingState;
        [SerializeField] private int _rangeCooldownState;
        [field: SerializeField] public GameObject AriaTypeSkill { get; private set; }
        
        private LifeStillView _view;
        private ISkillUser _skillUser;

        public event Action<string> ChangedState;

        public IStateSkill CurrentState { get; private set; }

        public ReadyState ReadyState { get; private set; }

        public UsingState UsingState { get; private set; }

        public CooldownState CooldownState { get; private set; }

        private void Awake()
        {
            _view = GetComponent<LifeStillView>();
            _skillUser = GetComponent<ISkillUser>();

            LifeStillTarget lifeStillTarget = new LifeStillTarget();
            TargetSearcher targetSearcher = new TargetSearcher(lifeStillTarget);

            ReadyState = new ReadyState(_skillUser, _rangeReadyState, _targetLayer, targetSearcher);
            UsingState = new UsingState(lifeStillTarget, _skillUser, _useTime, _rangeUsingState, _targetLayer, _damage, targetSearcher, _view);
            CooldownState = new CooldownState(_skillUser, _cooldown, _rangeCooldownState);
        }

        private void OnEnable()
        {            
            ReadyState.ChangedState += SelectState;
            UsingState.ChangedState += SelectState;
            CooldownState.ChangedState += SelectState;
        }

        private void Start()
        {
            AriaTypeSkill = Instantiate(AriaTypeSkill);

            AriaTypeSkill.transform.parent = _skillUser.UserTransform;

            CurrentState = ReadyState;
            SelectState(SkillStateType.Ready);
        }

        private void OnDisable()
        {
            ReadyState.ChangedState -= SelectState;
            UsingState.ChangedState -= SelectState;
            CooldownState.ChangedState -= SelectState;
        }

        private void Update()
        {
            CurrentState.Update();
        }

        private void ChangeState(IStateSkill newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void SelectState(SkillStateType stateType)
        {
            switch (stateType)
            {
                case SkillStateType.Ready:
                    ChangeState(ReadyState);
                    ChangedState?.Invoke(SkillStateType.Ready.ToString());
                    break;

                case SkillStateType.Using:
                    ChangeState(UsingState);
                    ChangedState?.Invoke(SkillStateType.Using.ToString());
                    break;

                case SkillStateType.Cooldown:
                    ChangeState(CooldownState);
                    ChangedState?.Invoke(SkillStateType.Cooldown.ToString());
                    break;

                default:
                    Console.WriteLine("None State");
                    break;
            }
        }
    }
}
