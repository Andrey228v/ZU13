using Assets.Scripts.Service;
using UnityEngine;

public abstract class State
{
    protected EnemyBody Enemy;
    protected RaycastHit2D Hit;
    protected float RadiusFOV;
    protected bool IsHited = false;
    protected bool IsPlayerFound = false;
    protected ITarget Player;
    protected Vector3 LastPosition;
    private Color _rayColor;
    private Color _inHitColor = Color.yellow;
    private Color _outHitColor = Color.red;
    
    public State(EnemyBody enemy, float radiusFOV)
    {
        Enemy = enemy;
        RadiusFOV = radiusFOV;
        Enemy.Move.SetMoveDirection(Enemy.Renderer.flipX ? Constants.LeftMoveDirection : Constants.RightMoveDirection);
    }

    public virtual void Enter() 
    {
        Enemy.Move.SetMoveDirection(Enemy.Renderer.flipX ? Constants.RightMoveDirection : Constants.LeftMoveDirection);
    }

    public virtual void Exit() { }

    public virtual void Update() 
    {
        DrawRaycast();

        Debug.DrawRay(Enemy.transform.position, Enemy.Move.MoveDirection.normalized, Color.black);

        if (Enemy.transform.localPosition.x <= LastPosition.x && Enemy.Move.MoveDirection == Constants.RightMoveDirection)
        {
            Enemy.Renderer.flipX = true;
            Enemy.Move.SetMoveDirection(Constants.LeftMoveDirection);
            Enemy.DamageDealer.AttackAria.offset = new Vector2(Enemy.DamageDealer.AttackAria.offset.x * Constants.Flip, Enemy.DamageDealer.AttackAria.offset.y);
        }
        else if (Enemy.transform.localPosition.x > LastPosition.x && Enemy.Move.MoveDirection == Constants.LeftMoveDirection)
        {
            Enemy.Renderer.flipX = false;
            Enemy.Move.SetMoveDirection(Constants.RightMoveDirection);
            Enemy.DamageDealer.AttackAria.offset = new Vector2(Enemy.DamageDealer.AttackAria.offset.x * Constants.Flip, Enemy.DamageDealer.AttackAria.offset.y);
        }

        LastPosition = Enemy.transform.localPosition;
    }

    public virtual void DrawRaycast() 
    {
        Collider2D collider = Physics2D.OverlapCircle(Enemy.transform.position, RadiusFOV, Enemy.TargetLayer);

        if (collider != null)
        {
            Hit = Physics2D.Raycast(Enemy.EyePosition.position, collider.gameObject.transform.position - Enemy.EyePosition.position, Vector3.Distance(Enemy.EyePosition.position, collider.gameObject.transform.position));

            IsPlayerFound = Hit.collider.TryGetComponent(out ITarget player);

            _rayColor = IsPlayerFound? _inHitColor: _outHitColor;

            Debug.DrawRay(Enemy.EyePosition.position, collider.gameObject.transform.position - Enemy.EyePosition.position, _rayColor);

            Player = player;
            IsHited = true;
        }
        else
        {
            IsHited = false;
        }
    }

    public virtual void TriggerEnter(Collider2D collider) { }

    public virtual void DrawGizmos() { }
}
