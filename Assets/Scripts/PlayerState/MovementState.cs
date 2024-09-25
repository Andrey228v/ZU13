using Assets.Scripts.PlayerState;
using Assets.Scripts.Service;
using UnityEngine;

public class MovementState : StatePlayer
{
    public MovementState(Player player) : base(player){}

    public override void Update()
    {
        base.Update();
 
        if (_player.UserInput.Attack) 
        {
            _player.ChangeState(PlayerStateType.Attack);
        }
    }
}
