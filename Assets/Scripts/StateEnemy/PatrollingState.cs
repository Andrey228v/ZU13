using UnityEngine;

public class PatrollingState : State
{
    private Transform[] _places;
    private int _placePointIndex = 0;

    public Transform NextPlace { get; private set; }

    private float _distanceError = 0.1f;

    public PatrollingState(EnemyBody enemy) : base(enemy){}

    public override void Enter()
    {
        base.Enter();

        _radiusFOV = 5;

        _places = new Transform[_enemy.PatrolRoute.childCount];

        for (int i = 0; i < _enemy.PatrolRoute.childCount; i++)
        {
            _places[i] = _enemy.PatrolRoute.GetChild(i);
        }

        _enemy.transform.position = _places[0].position;
        _lastPosition = _enemy.transform.position;

        NextPlace = _places[_placePointIndex];
    }


    public override void Exit()
    {
        base.Exit();
        _lastPosition = _places[0].position;
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(_enemy.transform.position, NextPlace.position) > _distanceError)
        {
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, NextPlace.position, _enemy.Speed * Time.deltaTime);
        }
        else
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        _placePointIndex = ++_placePointIndex % _places.Length;
        NextPlace = _places[_placePointIndex];
    }

    public override void DrawRaycst()
    {
        base.DrawRaycst();

        if (_isHited)
        {
            if (_hit.collider != null && _enemy.IsTargetInFOV == false)
            {
                if (isPlayerFound)
                {
                    _player.DetectedByEnemy();
                    _enemy.SetTargetInFOV(true);
                    _enemy.SetTargetPlayer(_player);

                    _enemy.ChangeState(EnemyStateType.Persecution);
                }
            }
        }
        else if (_enemy.IsTargetInFOV == true)
        {
            _enemy.Target.UndetectedByEnemy();
            _enemy.SetTargetInFOV(false);
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(_enemy.transform.position, _radiusFOV);
    }
}
