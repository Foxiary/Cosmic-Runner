using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.StopSlide();

    }

    public override void Update()
    {
        base.Update();
        if(GameManager.instance.gameState == GameState.Playing)
        {
            if (player.IsGrounded())
            {
                //jump
                if (Input.GetKeyDown(KeyCode.Space) || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Up) 
                {
                    player.swipeDetection.currentSwipe = SwipeDetection.SwipeDirection.None;
                    stateMachine.ChangeState(player.jumpState);
                    return;
                }
                //slide
                if (player.canSlide)
                {
                    if(Input.GetKeyDown(KeyCode.S) || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Down)
                    {
                        player.swipeDetection.currentSwipe = SwipeDetection.SwipeDirection.None;
                        stateMachine.ChangeState(player.slideState);
                    }

                }
            }
        }

        if (GameManager.instance.gameState == GameState.GameOver)
            stateMachine.ChangeState(player.deathState);
    }
}
