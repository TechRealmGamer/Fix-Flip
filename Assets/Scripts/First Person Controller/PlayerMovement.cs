using UnityEngine;

namespace LuciferGamingStudio
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController characterController;
        
        [SerializeField]
        private float gravity = -9.81f;
        [SerializeField]
        private float walkSpeed = 5f;
        [SerializeField]
        private float sprintSpeed = 10f;
        [SerializeField]
        private float jumpHeight = 1.5f;

        private bool isGrounded;
        private Vector3 playerVelocity;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;

            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -1f; // Small negative value to keep the player grounded

            Vector2 input = InputManager.Instance.GetMovementInput();
            Vector3 move = transform.right * input.x + transform.forward * input.y;
            
            float currentSpeed = InputManager.Instance.IsSprintPressed() ? sprintSpeed : walkSpeed;

            characterController.Move(move.normalized * currentSpeed * Time.deltaTime);
            
            if (InputManager.Instance.IsJumpPressedInThisFrame() && isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            playerVelocity.y += gravity * Time.deltaTime;

            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }
}
