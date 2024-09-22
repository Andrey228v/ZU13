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

        _places = new Transform[_enemy.PatrolRoute.childCount];

        for (int i = 0; i < _enemy.PatrolRoute.childCount; i++)
        {
            _places[i] = _enemy.PatrolRoute.GetChild(i);
        }

        _enemy.transform.position = _places[0].position;
        LastPosition = _enemy.transform.position;

        NextPlace = _places[_placePointIndex];
    }


    public override void Exit()
    {
        base.Exit();
        LastPosition = _places[0].position;
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
            Debug.Log("SMENA");
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

        Collider2D collider = Physics2D.OverlapCircle(_enemy.transform.position, _radiusFOV, _enemy.TargetLayer);

        if (collider != null)
        {
            _hit = Physics2D.Raycast(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, Vector3.Distance(_enemy.EyePosition.position, collider.gameObject.transform.position));

            Debug.DrawRay(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, Color.red);

            Debug.Log(_hit.collider);

            if (_hit.collider != null && _enemy.IsTargetInFOV == false)
            {
                bool isPlayerFound = _hit.collider.TryGetComponent(out Player player);

                if (isPlayerFound)
                {
                    player.DetectedByEnemy();
                    _enemy.SetTargetInFOV(true);
                    _enemy.SetTargetPlayer(player);

                    _enemy.ChangeState(EnemyStateType.Persecution);
                }
            }
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(_enemy.transform.position, _radiusFOV);
    }
}
