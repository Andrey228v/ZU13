using UnityEngine;

namespace Assets.Scripts.PlayerState
{
    public class StateMachinePlayer
    {
        public StateMachinePlayer(Player player)
        {
            MovementState = new MovementState(player);
            AttackStatePlayer = new AttackState(player);
        }

        public StatePlayer CurrentState { get; private set; }
        public StatePlayer PastState { get; private set; }
        public MovementState MovementState { get; private set; }
        public AttackState AttackStatePlayer { get; private set; }

        public void Initialize(PlayerStateType stateType)
        {
            CurrentState = MovementState;
            CurrentState.Enter();
        }

        public void ChangeState(StatePlayer newState)
        {
            CurrentState.Exit();
            PastState = CurrentState;
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void SelectState(PlayerStateType stateType)
        {
            switch (stateType)
            {
                case PlayerStateType.Movement:
                    ChangeState(MovementState);
                    break;

                case PlayerStateType.Attack:
                    ChangeState(AttackStatePlayer);
                    break;

                default:
                    Debug.Log("Такого состояния нет");
                    break;
            }
        }
    }
}
