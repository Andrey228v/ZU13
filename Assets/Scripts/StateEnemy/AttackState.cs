using Assets.Scripts.Service;
using UnityEngine;

public class AttackState : State
{
    private bool _isAttack = false;
    private Attack _attack = new Attack();

    public AttackState(EnemyBody enemy, float radiusFOV) : base(enemy, radiusFOV) 
    {
        RadiusFOV = radiusFOV;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.Animator.SetBool(EnemyAnimations.AnimatorParameterAttack, true);
        _isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
        Enemy.Animator.SetBool(EnemyAnimations.AnimatorParameterAttack, false);
    }

    public override void Update()
    {
        base.Update();

        if (Enemy.Target.Dead.IsDead == false)
        {
            Enemy.Move.Action(Enemy.Target.GetPosition());
        }
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();

        if (IsHited)
        {
            if (Hit.collider != null && IsPlayerFound == true && Hit.distance > Enemy.DamageDealer.AttackDistance)
            {
                Enemy.StateMachine.SelectState(EnemyStateType.Persecution);
            }
            else if (Hit.collider != null && IsPlayerFound == false)
            {
                Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
            }
        }
        else if (Enemy.GetIsTargetInFOV() == true)
        {
            Enemy.StateMachine.SelectState(EnemyStateType.Persecution);
        }
        else if (Enemy.GetIsTargetInFOV() == false)
        {
            Enemy.StateMachine.SelectState(EnemyStateType.Patrolling);
        }
    }

    public override void TriggerEnter(Collider2D collider)
    {
        base.TriggerEnter(collider);

        if (_isAttack == false)
        {
            _attack.SetAttack(Enemy, collider);
        }
    }
    
    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(Enemy.transform.position, RadiusFOV);
    }
}
