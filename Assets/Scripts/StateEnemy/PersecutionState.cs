using UnityEngine;

public class PersecutionState : State
{
    private const string AnimatorParameterPersecution = "Persecution";

    public PersecutionState(EnemyBody enemy, float radiusFOV) : base(enemy, radiusFOV) 
    {
        _radiusFOV = radiusFOV;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.Animator.SetBool(AnimatorParameterPersecution, true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.Animator.SetBool(AnimatorParameterPersecution, false);
    }

    public override void Update()
    {
        base.Update();

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.Target.GetPosition(), _enemy.Speed * Time.deltaTime);
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();

        if (_isHited) 
        {
            if (_hit.collider != null && _enemy.IsTargetInFOV == true)
            {
                if (isPlayerFound == false)
                {
                    _enemy.Target.UndetectedByEnemy();
                    _enemy.SetTargetInFOV(false);
                    _enemy.ChangeState(EnemyStateType.Patrolling);
                }
                if (_hit.distance < _enemy.AttackDistance)
                {
                    _enemy.ChangeState(EnemyStateType.Attack);
                }
            }

            else if (_hit.collider != null && _enemy.IsTargetInFOV == false)
            {
                _enemy.Target.UndetectedByEnemy();
                _enemy.ChangeState(EnemyStateType.Patrolling);
            }
        }
        else if (_enemy.IsTargetInFOV == true)
        {
            _enemy.Target.UndetectedByEnemy();
            _enemy.SetTargetInFOV(false);
            _enemy.ChangeState(EnemyStateType.Patrolling);
        }
        else if( _enemy.IsTargetInFOV == false)
        {
            _enemy.Target.UndetectedByEnemy();
            _enemy.ChangeState(EnemyStateType.Patrolling);
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(_enemy.transform.position, _radiusFOV);
    }
}
