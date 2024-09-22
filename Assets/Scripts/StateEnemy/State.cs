using UnityEngine;

public abstract class State
{
    protected EnemyBody _enemy;
    protected RaycastHit2D _hit;
    protected float _radiusFOV = 5;
    protected Vector2 _rightMoveDirection = new Vector2(-1, 0);
    protected Vector2 _leftMoveDirection = new Vector2(1, 0);

    protected Vector3 LastPosition { get; set; }

    public State(EnemyBody enemy)
    {
        _enemy = enemy;
        _enemy.SetMoveDirection(_enemy.Renderer.flipX ? _rightMoveDirection : _leftMoveDirection);
    }

    public virtual void Enter() 
    {
        _enemy.SetMoveDirection(_enemy.Renderer.flipX ? _rightMoveDirection : _leftMoveDirection);
    }

    public virtual void Exit() { }

    public virtual void Update() 
    {
        if (_enemy.transform.localPosition.x <= LastPosition.x && _enemy.MoveDirectoin == _leftMoveDirection)
        {
            _enemy.Renderer.flipX = true;
            _enemy.SetMoveDirection(_rightMoveDirection);

            _enemy.AttackAria.transform.localPosition = new Vector2(_enemy.AttackAria.transform.localPosition.x * -1, _enemy.AttackAria.transform.localPosition.y);
        }
        else if (_enemy.transform.localPosition.x > LastPosition.x && _enemy.MoveDirectoin == _rightMoveDirection)
        {
            _enemy.Renderer.flipX = false;
            _enemy.SetMoveDirection(_leftMoveDirection);

            _enemy.AttackAria.transform.localPosition = new Vector2(_enemy.AttackAria.transform.localPosition.x * -1, _enemy.AttackAria.transform.localPosition.y);
        }

        LastPosition = _enemy.transform.localPosition;
    }

    public virtual void DrawRaycst() { }

    public virtual void TriggerEnter(Collider2D collider) { }

    public virtual void DrawGizmos() { }
}
