using UnityEngine;

public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.canSlide = false;
        AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);

    }

    public override void Exit()
    {
        base.Exit();
        player.canSlide = true;

    }

    public override void Update()
    {
        base.Update();
        if (slideStartTrigger)
            player.StartSlide();

        if (triggerCalled)
        {
            player.StopSlide();
            stateMachine.ChangeState(player.moveState);

        }

    }

}
