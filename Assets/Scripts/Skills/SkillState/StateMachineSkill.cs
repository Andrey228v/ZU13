namespace Assets.Scripts.Skills.SkillState
{
    public class StateMachineSkill
    {
        public StateMachineSkill(LifeStillModel model)
        {}

        public StateSkill CurrentState { get; private set; }

        public void Initialize(StateSkill stateType)
        {
            CurrentState = stateType;
            CurrentState.Enter();
        }

        public void ChangeState(StateSkill newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
