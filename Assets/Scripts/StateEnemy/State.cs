using Assets.Scripts.Service;
using UnityEngine;

public abstract class State
{
    protected EnemyBody _enemy;
    protected RaycastHit2D _hit;
    protected float _radiusFOV;
    
    protected bool _isHited = false;
    protected bool isPlayerFound = false;
    protected ITarget _player;
    protected Vector3 _lastPosition;

    private Color _rayColor;
    private Color _inHitColor = Color.yellow;
    private Color _outHitColor = Color.red;
    private int _flip = -1;

    public State(EnemyBody enemy, float radiusFOV)
    {
        _enemy = enemy;
        _radiusFOV = radiusFOV;
        _enemy.SetMoveDirection(_enemy.Renderer.flipX ? Constants.RightMoveDirection : Constants.LeftMoveDirection);
    }

    public virtual void Enter() 
    {
        _enemy.SetMoveDirection(_enemy.Renderer.flipX ? Constants.RightMoveDirection : Constants.LeftMoveDirection);
    }

    public virtual void Exit() { }

    public virtual void Update() 
    {
        DrawRaycast();

        if (_enemy.transform.localPosition.x <= _lastPosition.x && _enemy.MoveDirectoin == Constants.LeftMoveDirection)
        {
            _enemy.Renderer.flipX = true;
            _enemy.SetMoveDirection(Constants.RightMoveDirection);
            _enemy.AttackAria.offset = new Vector2(_enemy.AttackAria.offset.x * _flip, _enemy.AttackAria.offset.y);
        }
        else if (_enemy.transform.localPosition.x > _lastPosition.x && _enemy.MoveDirectoin == Constants.RightMoveDirection)
        {
            _enemy.Renderer.flipX = false;
            _enemy.SetMoveDirection(Constants.LeftMoveDirection);
            _enemy.AttackAria.offset = new Vector2(_enemy.AttackAria.offset.x * _flip, _enemy.AttackAria.offset.y);
        }

        _lastPosition = _enemy.transform.localPosition;
    }

    public virtual void DrawRaycast() 
    {
        Collider2D collider = Physics2D.OverlapCircle(_enemy.transform.position, _radiusFOV, _enemy.TargetLayer);

        if (collider != null)
        {
            _hit = Physics2D.Raycast(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, Vector3.Distance(_enemy.EyePosition.position, collider.gameObject.transform.position));

            isPlayerFound = _hit.collider.TryGetComponent(out ITarget player);

            _rayColor = isPlayerFound? _inHitColor: _outHitColor;

            Debug.DrawRay(_enemy.EyePosition.position, collider.gameObject.transform.position - _enemy.EyePosition.position, _rayColor);

            _player = player;
            _isHited = true;
        }
        else
        {
            _isHited = false;
        }
    }

    public virtual void TriggerEnter(Collider2D collider) { }

    public virtual void DrawGizmos() { }
}
