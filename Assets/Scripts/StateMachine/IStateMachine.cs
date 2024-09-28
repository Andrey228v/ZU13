namespace Assets.Scripts.StateMachine
{
    public interface IStateMachine
    {
        public IState CurrentState { get;}

        public IState PastState { get;}

        public void Initialize(IState startState);

        public void ChangeState(IState newState);
    }
}
