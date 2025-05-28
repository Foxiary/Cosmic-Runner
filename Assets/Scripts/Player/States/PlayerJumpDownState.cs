using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownState : PlayerState
{
    public PlayerJumpDownState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpDown();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player.IsGrounded())
            stateMachine.ChangeState(player.slideState);

        if (GameManager.instance.gameState == GameState.GameOver)
            stateMachine.ChangeState(player.deathState);

    }
}
