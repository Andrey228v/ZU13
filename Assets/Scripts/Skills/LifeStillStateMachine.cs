using Assets.Scripts.Skills.SkillState;
using System;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(ISkillUser), typeof(LifeStillView))]
    public class LifeStillStateMachine : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private int _useTime;
        [SerializeField] private int _cooldown;
        [SerializeField] private int _damage;
        [SerializeField] private int _range;
        
        public event Action<string> ChangedState;
        public event Action<int> ChangedRange;

        private LifeStillView _view;
        private ISkillUser _skillUser;

        public IStateSkill CurrentState { get; private set; }

        public ReadyState ReadyState { get; private set; }

        public UsingState UsingState { get; private set; }

        public CooldownState CooldownState { get; private set; }

        private void Awake()
        {
            _view = GetComponent<LifeStillView>();
            _skillUser = GetComponent<ISkillUser>();
        }

        private void OnEnable()
        {
            LifeStillTarget lifeStillTarget = new LifeStillTarget();
            TargetSearcher targetSearcher = new TargetSearcher(lifeStillTarget);

            ReadyState = new ReadyState(lifeStillTarget, _skillUser, _range, _targetLayer, targetSearcher);
            UsingState = new UsingState(lifeStillTarget, _skillUser, _useTime, _range, _targetLayer, _damage, targetSearcher, _view);
            CooldownState = new CooldownState(lifeStillTarget, _skillUser, _cooldown);
            
            ReadyState.ChangedState += SelectState;
            UsingState.ChangedState += SelectState;
            CooldownState.ChangedState += SelectState;
        }

        private void Start()
        {
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
                    ChangedState?.Invoke(SkillStateType.Ready.ToString());
                    ChangedRange?.Invoke(_range);
                    ChangeState(ReadyState);
                    break;

                case SkillStateType.Using:
                    ChangeState(UsingState);
                    ChangedRange?.Invoke(_range);
                    break;

                case SkillStateType.Cooldown:
                    ChangeState(CooldownState);
                    ChangedRange?.Invoke(0);
                    break;

                default:
                    Console.WriteLine("None State");
                    break;
            }
        }
    }
}
