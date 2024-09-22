namespace Assets.Scripts.PlayerState
{
    public class StateMachinePlayer
    {
        public StatePlayer CurrentState { get; private set; }
        public StatePlayer PastState { get; private set; }

        public void Initialize(StatePlayer startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(StatePlayer newState)
        {
            CurrentState.Exit();
            PastState = CurrentState;
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
