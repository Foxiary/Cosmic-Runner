using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public SwipeDetection swipeDetection {  get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    [HideInInspector] public CharacterController controller;
    [SerializeField] CapsuleCollider trigger;

    [Header("Gravity")]
    public float gravity = -30f;
    public float jumpHeight;
    public float jumpDownHeight;
    [HideInInspector] public float verticalVelocity;

    [Header("Move Info")]
    [SerializeField] int landDistance;
    [SerializeField] float landSpeed;
    [SerializeField] private LayerMask groundLayer;

    public int currentLand = 1;
    public bool canSlide = true;

    [Header("Item Info")]
    [HideInInspector] public JetpackBootster jetpackBooster;
    public GameObject fireParticle;
    public GameObject magnetTrigger;

    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerJumpDownState jumpDownState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpLandState jumpLandState { get; private set; }
    public PlayerMoveLandState moveLandState { get; private set; }
    public PlayerSlideState slideState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    public PlayerJetpackState jetpackState { get; private set; }

    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        swipeDetection = GetComponent<SwipeDetection>();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        moveLandState = new PlayerMoveLandState(this, stateMachine, "MoveLand");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        jumpLandState = new PlayerJumpLandState(this, stateMachine, "JumpLand");
        jumpDownState = new PlayerJumpDownState(this, stateMachine, "Air");
        airState = new PlayerAirState(this, stateMachine, "Air");
        slideState = new PlayerSlideState(this, stateMachine, "Slide");
        deathState = new PlayerDeathState(this, stateMachine, "Death");

        //Item Boost State
        jetpackState = new PlayerJetpackState(this, stateMachine, "Jetpack");

    }
    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            stateMachine.Initialize(idleState);
        }
        else
            stateMachine.currentState.Update();

        if(currentLand != LandUpdate())
            currentLand = LandUpdate();

        ApplieGravity();

        LockZPos();

        Movement();

        UpdateAnimationSpeed();
    }
        
    private void UpdateAnimationSpeed()
    {
        anim.speed = .9f + GameManager.instance.roadManager.currentSpeed / 100;
    }
    #region Movement
    private void LockZPos()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }

    private void Movement()
    {
        Vector3 targetPosition = transform.position;

        switch (currentLand)
        {
            case 0:
                targetPosition = new Vector3(-landDistance, transform.position.y, transform.position.z);
                break;
            case 1:
                targetPosition = new Vector3(0, transform.position.y, transform.position.z);
                break;
            case 2:
                targetPosition = new Vector3(landDistance, transform.position.y, transform.position.z);
                break;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, landSpeed * Time.deltaTime);
    }
    private int LandUpdate() => Mathf.Clamp(currentLand, 0, 2);
    public bool LandCheck()
    {
        if (currentLand >= 0 && currentLand <= 2)
            return true;
        return false;
    }
    public void MoveLeft()
    {
        currentLand--;
        if(LandCheck())
            AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
    }

    public void MoveRight()
    {
        currentLand++;
        if (LandCheck())
            AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
    }

    public void DeathForce()
    {

    }
    #endregion
    #region Slide
    public void StartSlide()
    {
        //UpdateCapsuleCollider(0.3f, 0.4f, new Vector3(0, 0.2f, 0));
        UpdateTriggerCollider(0.3f, new Vector3(0, 0.1f, 0));
    }

    public void StopSlide()
    {
        //UpdateCapsuleCollider(1.85f, 0.6f, new Vector3(0, 0.85f, 0));
        UpdateTriggerCollider(1.5f, new Vector3(0, 1.1f, 0));
    }

    private void UpdateCapsuleCollider(float height, float radius, Vector3 center)
    {
        controller.height = height;
        controller.radius = radius;
        controller.center = center;
    }

    private void UpdateTriggerCollider(float height, Vector3 center)
    {
        trigger.height = height;
        trigger.center = center;
    }

    #endregion
    #region Gravity Info 
    public void JumpOnGround()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * this.gravity);
    }
    public void JumpDown()
    {
        verticalVelocity = -Mathf.Sqrt(jumpDownHeight * -2f * this.gravity);
    }
    private void ApplieGravity()
    {
        if(jetpackBooster == null)
        {
            if (!IsGrounded())
                verticalVelocity += this.gravity * Time.deltaTime;

            Vector3 gravity = new Vector3(0, verticalVelocity, 0);
            controller.Move(gravity * Time.deltaTime);
        }
    }
    #endregion
    public bool IsGrounded() => controller.isGrounded;
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SlideStartTrigger() => stateMachine.currentState.SlideStartTrigger();
    public void SlideEndTrigger() => stateMachine.currentState.SlideEndTrigger();
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.5f, 0), 0.3f);
    }

}

