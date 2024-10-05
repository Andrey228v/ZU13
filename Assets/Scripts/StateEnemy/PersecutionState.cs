using Assets.Scripts.Service;
using UnityEngine;

public class PersecutionState : State
{
    public PersecutionState(EnemyBody enemy, float radiusFOV) : base(enemy, radiusFOV) 
    {
        RadiusFOV = radiusFOV;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.Animator.SetBool(EnemyAnimations.AnimatorParameterPersecution, true);

        if(Enemy.Target.Dead.IsDead == true)
        {
            Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Enemy.Animator.SetBool(EnemyAnimations.AnimatorParameterPersecution, false);
    }

    public override void Update()
    {
        base.Update();

        Enemy.Move.Action(Enemy.Target.GetPosition());
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();

        if (IsHited) 
        {
            if (Hit.collider != null && Enemy.GetIsTargetInFOV() == true)
            {
                if (IsPlayerFound == false)
                {
                    Enemy.Target.UndetectedByEnemy();
                    Enemy.SetTargetInFOV(false);
                    Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
                }
                if (Hit.distance < Enemy.DamageDealer.AttackDistance)
                {
                    Enemy.StateMachine.SelectState(EnemyStateType.Attack);
                }
            }

            else if (Hit.collider != null && Enemy.GetIsTargetInFOV() == false)
            {
                Enemy.Target.UndetectedByEnemy();
                Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
            }
        }
        else if (Enemy.GetIsTargetInFOV() == true)
        {
            Enemy.Target.UndetectedByEnemy();
            Enemy.SetTargetInFOV(false);
            Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
        }
        else if( Enemy.GetIsTargetInFOV() == false)
        {
            Enemy.Target.UndetectedByEnemy();
            Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
        }
        else
        {
            Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(Enemy.transform.position, RadiusFOV);
    }
}
