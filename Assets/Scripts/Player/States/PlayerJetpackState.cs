using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpackState : PlayerState
{
    private float gravitySave;
    private float speedSave;
    private Vector3 velocity;
    public PlayerJetpackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        gravitySave = player.gravity;
        player.gravity = 0;
        stateTimer = player.jetpackBooster.itemTime;
        speedSave = GameManager.instance.roadManager.currentSpeed;
        GameManager.instance.roadManager.currentSpeed = GameManager.instance.roadManager.speed * 4;

        player.fireParticle.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.jetpackBooster = null;
        player.gravity = gravitySave;
        GameManager.instance.roadManager.currentSpeed = speedSave;

        player.fireParticle.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 || player.jetpackBooster == null)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        //fly logic
        if (player.controller.transform.position.y < player.jetpackBooster.flyHeight)
        {
            velocity = new Vector3(0, 25 * Time.deltaTime, 0);
            player.controller.Move(velocity);
        }
        else
        {
            velocity = Vector3.zero;
        }
    }
}
