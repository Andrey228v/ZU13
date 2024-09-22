using UnityEngine;

public class PersecutionState : State
{
    private const string AnimatorParameterPersecution = "Persecution";

    public PersecutionState(EnemyBody enemy) : base(enemy)
    {
        _radiusFOV = 10f;
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

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.Target.transform.position, _enemy.Speed * Time.deltaTime);
    }

    public override void DrawRaycst()
    {
        base.DrawRaycst();

        Collider2D collider = Physics2D.OverlapCircle(_enemy.transform.position, _radiusFOV, _enemy.TargetLayer);

        if (collider != null)
        {
            _hit = Physics2D.Raycast(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, Vector3.Distance(_enemy.EyePosition.position, collider.gameObject.transform.position));

            Debug.DrawRay(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, Color.yellow);

            if (_hit.collider != null && _enemy.IsTargetInFOV == true)
            {
                bool isPlayerFoundVision = _hit.collider.TryGetComponent(out Player player);

                if (isPlayerFoundVision == false)
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
