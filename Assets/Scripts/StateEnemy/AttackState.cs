using Assets.Scripts.Service;
using UnityEngine;

public class AttackState : State
{
    private const string AnimatorParameterAttack = "Attack";

    public AttackState(EnemyBody enemy, float radiusFOV) : base(enemy, radiusFOV) 
    {
        _radiusFOV = radiusFOV;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.Animator.SetBool(AnimatorParameterAttack, true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.Animator.SetBool(AnimatorParameterAttack, false);
    }

    public override void Update()
    {
        base.Update();

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.Target.GetPosition(), _enemy.Speed * Time.deltaTime);
    }

    public override void DrawRaycst()
    {
        base.DrawRaycst();

        if (_isHited)
        {
            if (_hit.collider != null && isPlayerFound == true && _hit.distance > _enemy.AttackDistance)
            {
                _enemy.ChangeState(EnemyStateType.Persecution);
            }
            else if (_hit.collider != null && isPlayerFound == false)
            {
                _enemy.ChangeState(EnemyStateType.Patrolling);
            }
        }
        else if (_enemy.IsTargetInFOV == true)
        {
            _enemy.ChangeState(EnemyStateType.Persecution);
        }
        else if (_enemy.IsTargetInFOV == false)
        {
            _enemy.ChangeState(EnemyStateType.Patrolling);
        }
    }

    public override void TriggerEnter(Collider2D collider)
    {
        base.TriggerEnter(collider);

        if (collider.TryGetComponent(out IMoveUnit body))
        {
            _enemy.SetDamageDirection(-body.MoveDirectoin);

            if (collider.TryGetComponent(out IDamageTaker target))
            {
                target.GetDamage(_enemy.Damage);

                if (collider.TryGetComponent(out ITypeDamage typeDamage))
                {
                    typeDamage.AttackDealer(_enemy);
                }
            }
        }
    }
    
    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(_enemy.transform.position, _radiusFOV);
    }
}
