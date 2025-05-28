using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;
    protected Animator anim;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;
    protected bool slideEndTrigger;
    protected bool slideStartTrigger;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        slideStartTrigger = false;
        slideEndTrigger = false;
        anim = player.anim;

        player.anim.SetBool(animBoolName, true);

    }
    public virtual void Update()
    {
        if(stateTimer > 0) 
            stateTimer -= Time.deltaTime;

        if (anim == null)
            anim = player.anim;
        Movement();



    }

    private void Movement()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Left)
            {
                player.MoveLeft();
                player.swipeDetection.currentSwipe = SwipeDetection.SwipeDirection.None;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || player.swipeDetection.GetSwipeDirection() == SwipeDetection.SwipeDirection.Right)
            {
                player.MoveRight();
                player.swipeDetection.currentSwipe = SwipeDetection.SwipeDirection.None;
            }
        }
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public virtual void SlideEndTrigger()
    {
        slideEndTrigger = true;
    }
    public virtual void SlideStartTrigger()
    {
        slideStartTrigger = true;
    }
}
