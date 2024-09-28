using Assets.Scripts.PlayerState;

public class MovementState : StatePlayer
{
    public MovementState(Player player) : base(player){}

    public override void Update()
    {
        base.Update();
 
        if (_player.UserInput.Attack) 
        {
            _player.StateMachinePlayer.SelectState(PlayerStateType.Attack);
        }
    }
}
