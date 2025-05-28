using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGrounded() && (player.LandCheck()) && GameManager.instance.gameState == GameState.Playing)
        {
            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) 
                || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)
                || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Left 
                || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Right)
                    stateMachine.ChangeState(player.moveLandState);
        }
        if (!player.IsGrounded())
            stateMachine.ChangeState(player.airState);

    }
}
