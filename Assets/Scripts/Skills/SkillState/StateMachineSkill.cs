using System;

namespace Assets.Scripts.Skills.SkillState
{
    public class StateMachineSkill
    {
        public StateMachineSkill(LifeStillModel model)
        {
            ReadyState = new ReadyState(model);
            UsingState = new UsingState(model);
            CooldownState = new CooldownState(model);
        }

        public StateSkill CurrentState { get; private set; }

        public ReadyState ReadyState { get; private set; }

        public UsingState UsingState { get; private set; }

        public CooldownState CooldownState { get; private set; }

        public void Initialize(SkillStateType stateType)
        {
            CurrentState = ReadyState;
            CurrentState.Enter();
        }

        public void ChangeState(StateSkill newState)
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
                    break;

                case SkillStateType.Using:
                    ChangeState(UsingState);
                    break;

                case SkillStateType.Cooldown:
                    ChangeState(CooldownState);
                    break;

                default:
                    Console.WriteLine("Такого состояния нет");
                    break;
            }
        }

    }
}
