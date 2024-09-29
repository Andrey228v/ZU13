using System;

namespace Assets.Scripts.StateEnemy
{
    public class StateMachine
    {
        public StateMachine(EnemyBody enemy, float radiusFOVPatrolling, float radiusFOVPersecution, float radiusFOVAttack)
        {
            PatrollingState = new PatrollingState(enemy, radiusFOVPatrolling);
            PersecutionState = new PersecutionState(enemy, radiusFOVPersecution);
            AttackState = new AttackState(enemy, radiusFOVAttack);
        }

        public State CurrentState { get; private set; }
        public PatrollingState PatrollingState { get; private set; }
        public PersecutionState PersecutionState { get; private set; }
        public AttackState AttackState { get; private set; }

        public void Initialize(EnemyStateType stateType)
        {
            CurrentState = PatrollingState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void SelectState(EnemyStateType stateType)
        {
            switch (stateType)
            {
                case EnemyStateType.Patrolling:
                    ChangeState(PatrollingState);
                    break;

                case EnemyStateType.Persecution:
                    ChangeState(PersecutionState);
                    break;

                case EnemyStateType.Attack:
                    ChangeState(AttackState);
                    break;
                default:
                    Console.WriteLine("Такого состояния нет");
                    break;
            }
        }
    }
}
