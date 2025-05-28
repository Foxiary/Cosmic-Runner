using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StopSlide();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player.IsGrounded())
            stateMachine.ChangeState(player.jumpLandState);

        if (!player.IsGrounded())
            if (Input.GetKeyDown(KeyCode.S) || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Down)
            {
                player.swipeDetection.currentSwipe = SwipeDetection.SwipeDirection.None;
                stateMachine.ChangeState(player.jumpDownState);
            }

        if (GameManager.instance.gameState == GameState.GameOver)
            stateMachine.ChangeState(player.deathState);
    }
}
