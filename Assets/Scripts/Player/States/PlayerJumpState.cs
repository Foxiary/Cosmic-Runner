public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.JumpOnGround();
        AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (GameManager.instance.isDeath)
            stateMachine.ChangeState(player.deathState);
    }
}
