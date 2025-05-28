using UnityEngine;

namespace ReadyPlayerMe.Samples.QuickStart
{
    [RequireComponent(typeof(ThirdPersonMovement),typeof(PlayerInput))]
    public class ThirdPersonController : MonoBehaviour
    {
        private const float FALL_TIMEOUT = 0.15f;
            
        private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private static readonly int JumpHash = Animator.StringToHash("JumpTrigger");
        private static readonly int FreeFallHash = Animator.StringToHash("FreeFall");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int SlideHash = Animator.StringToHash("SlideTrigger");
        private static readonly int LeanHash = Animator.StringToHash("Leaning");
        
        private Animator animator;
        private GameObject avatar;
        private ThirdPersonMovement player;

        public float speed;
        private float fallTimeoutDelta;
        
        [SerializeField][Tooltip("Useful to toggle input detection in editor")]
        private bool isInitialized;

        private void Init()
        {
            player = GetComponent<ThirdPersonMovement>();
            isInitialized = true;
        }
        private void InPutHandle()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnJump();
            if (Input.GetKeyDown(KeyCode.S))
                OnSlide();

            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                if(player.LandCheck())
                    animator.SetTrigger(LeanHash);
            }
        }
        public void Setup(GameObject target, RuntimeAnimatorController runtimeAnimatorController)
        {
            if (!isInitialized)
            {
                Init();
            }
            
            avatar = target;
            animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = runtimeAnimatorController;
            animator.applyRootMotion = false;
            
        }
        
        private void Update()
        {
            InPutHandle();
            if (avatar == null)
            {
                return;
            }
            UpdateSpeed();
            UpdateAnimator();
        }

        private void UpdateSpeed()
        {
            if (speed <= 8)
                speed += Time.deltaTime;
        }

        private void UpdateAnimator()
        {
            var isGrounded = player.IsGrounded();
            animator.SetFloat(MoveSpeedHash, speed);
            animator.SetBool(IsGroundedHash, isGrounded);
            if (isGrounded)
            {
                fallTimeoutDelta = FALL_TIMEOUT;
                animator.SetBool(FreeFallHash, false);
            }
            else
            {
                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool(FreeFallHash, true);
                }
            }
        }

        private void OnJump()
        {
            if (player.IsGrounded())
            {
                player.HandleJumpAndGravity();
                speed = 5;
                animator.SetTrigger(JumpHash);
            }
            else if (player.TryJump())
            {
                player.JumpFromSlide();
                speed = 5;
                animator.SetTrigger(JumpHash);
            }
        }
        private void OnSlide()
        {
            if (player.TrySlide())
            {
                speed = 5;
                animator.SetTrigger(SlideHash);
            }
        }
    }
}
