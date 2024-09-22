using Assets.Scripts.PlayerState;
using UnityEngine;

public class MovementState : StatePlayer
{
    public MovementState(Player player) : base(player){}

    public override void Update()
    {
        base.Update();
 
        if (Input.GetMouseButtonDown(0)) 
        {
            _player.ChangeState(PlayerStateType.Attack);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _player.Animator.SetFloat(AnimatorParameterSpeed, Mathf.Abs(_horizontalMove));
        Vector2 targetForce = new Vector2(_horizontalMove * _player.MaxSpeed, _player.Rigidbody.velocity.y);
        _player.Rigidbody.AddForce(targetForce, ForceMode2D.Impulse);
    }
}
