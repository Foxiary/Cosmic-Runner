using UnityEngine;
using UnityEngine.EventSystems;

namespace ReadyPlayerMe.Samples.QuickStart
{
    [RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
    public class ThirdPersonMovement : MonoBehaviour
    {
        [Header("Components")]
        private CharacterController controller;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] CapsuleCollider trigger;

        [Header("Gravity")]
        private float gravity = -18f;
        public float jumpHeight;
        private float verticalVelocity;
        private bool jumpTrigger;
        
        [Header("Move Info")]
        [SerializeField] int landDistance;
        [SerializeField] float moveSpeed;
        public int currentLand = 1;
        private bool IsSliding;
        private bool slideTrigger;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
        private void Update()
        {
            HandleJumpAndGravity();
            
            HandleInput();

        }
        private void FixedUpdate()
        {
            Movement();

            transform.position = new Vector3(transform.position.x, transform.position.y, -20) ;
        }
        #region Input Info
        private void HandleInput()
        {
            // Update land based on external logic
            currentLand = UpdateLand();

            // Handle movement logic
            HandleMovement();

            // Handle slide logic
            HandleSlide();
        }

        private void HandleMovement()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();

            }
        }
        private void MoveLeft()
        {
            currentLand--;
            if (LandCheck())
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
            }
        }

        private void MoveRight()
        {
            currentLand++;
            if (LandCheck())
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
            }
        }

        private void HandleSlide()
        {
            if (slideTrigger && IsGrounded())
            {
                StartSlide();
            }
            else if (IsSliding || GetComponent<ThirdPersonController>().speed >= 5.6f)
            {
                StopSlide();
            }
        }

        private void StartSlide()
        {
            IsSliding = true;
            UpdateCapsuleCollider(0.3f, 0.4f, new Vector3(0, 0.2f, 0));
            UpdateTriggerCollider(0.3f, new Vector3(0, 0.1f, 0));
        }

        private void StopSlide()
        {
            IsSliding = false;
            UpdateCapsuleCollider(1.85f, 0.6f, new Vector3(0, 0.85f, 0));
            UpdateTriggerCollider(1.7f, new Vector3(0, 1f, 0));
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

        // (Optional) Consider creating a dedicated method for land update logic if complex
        private int UpdateLand()
        {
            // Implement land update logic here (e.g., call LandUpdate())
            return LandUpdate(); // Assuming LandUpdate() returns the updated land value
        }

        #endregion

        public bool LandCheck()
        {
            if (currentLand >= 0 && currentLand <= 2)
                return true;
            return false;
        }
        #region Gravity Info 
        public void HandleJumpAndGravity()
        {
            // Apply gravity when not grounded
            if (!IsGrounded())
            {
                ApplyGravity();
            }

            // Handle jump on ground
            JumpOnGround();

            // Handle jump down (optional)
            JumpDown();

            // Move character based on vertical velocity
            MoveCharacter();
        }

        private void ApplyGravity()
        {
            verticalVelocity += this.gravity * Time.deltaTime;
        }

        private void JumpOnGround()
        {
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * this.gravity);
            }
        }

        private void JumpDown()
        {
            // Implement jump down logic here (optional)
            // if (!IsGrounded() && Input.GetKeyDown(KeyCode.S))
            // {
            //     AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
            //     verticalVelocity = -Mathf.Sqrt(jumpHeight * -2f * this.gravity) * 2;
            // }
        }

        private void MoveCharacter()
        {
            Vector3 gravity = new Vector3(0, verticalVelocity, 0);
            controller.Move(gravity * Time.deltaTime);
        }
        #endregion
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

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        public bool TryJump()
        {
            jumpTrigger = false;
            if (!IsGrounded())
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
                jumpTrigger = true;
            }
            return jumpTrigger;
        }
        public bool TrySlide()
        {
            slideTrigger = false;
            if (IsGrounded())
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.whooshed);
                slideTrigger = true;
            }
            return slideTrigger;
        }
        public bool IsGrounded() => controller.isGrounded;

        public void JumpFromSlide()
        {
            if (IsSliding)
            {
                IsSliding = false;
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * this.gravity);
            }
        }
        private int LandUpdate()
        {
            if (currentLand < 0)
                return 0;
            else if (currentLand > 2)
                return 2;
            return currentLand;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + new Vector3(0, -0.5f, 0), 0.3f);
        }
    }
}
