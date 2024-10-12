using UnityEngine;

public class PatrollingState : State
{
    private Transform[] _places;
    private int _placePointIndex = 0;
    private float _distanceError = 0.2f;
    private float _distanceDeviationFromZone = -1;
    public PatrollingState(EnemyBody enemy, float radiusFOV) : base(enemy, radiusFOV) 
    {
        RadiusFOV = radiusFOV;
    }

    public Transform NextPlace { get; private set; }

    public override void Enter()
    {
        base.Enter();

        _places = new Transform[Enemy.PatrolRoute.childCount];

        for (int i = 0; i < Enemy.PatrolRoute.childCount; i++)
        {
            _places[i] = Enemy.PatrolRoute.GetChild(i);
        }

        Enemy.transform.position = _places[0].position;
        LastPosition = Enemy.transform.position;
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

        if (Mathf.Abs(Vector3.Distance(Enemy.transform.position, NextPlace.position)) > _distanceError)
        {
            Enemy.Move.Action(NextPlace.position);
        }
        else
        {
            NextPoint();
        }

        if(Enemy.transform.position.y - _places[0].position.y < _distanceDeviationFromZone)
        {
            Enemy.transform.position = _places[0].position;
        }
    }

    private void NextPoint()
    {
        _placePointIndex = ++_placePointIndex % _places.Length;
        NextPlace = _places[_placePointIndex];
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();

        if (IsHited)
        {
            if (Hit.collider != null && Enemy.GetIsTargetInFOV() == false)
            {
                if (IsPlayerFound)
                {
                    Player.DetectedByEnemy();
                    Enemy.SetTargetInFOV(true);
                    Enemy.SetTargetPlayer(Player);

                    Enemy.StateMachine.SelectState(EnemyStateType.Persecution);
                }
            }
        }
        else if (Enemy.GetIsTargetInFOV() == true)
        {
            Enemy.Target.UndetectedByEnemy();
            Enemy.SetTargetInFOV(false);
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(Enemy.transform.position, RadiusFOV);
    }
}
