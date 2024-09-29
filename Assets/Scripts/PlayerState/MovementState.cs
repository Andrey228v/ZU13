using Assets.Scripts.PlayerState;

public class MovementState : StatePlayer
{
    public MovementState(Player player) : base(player){}

    public override void Update()
    {
        base.Update();
 
        if (Player.UserInput.Attack) 
        {
            Player.StateMachinePlayer.SelectState(PlayerStateType.Attack);
        }
    }
}
