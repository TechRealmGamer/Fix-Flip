using UnityEngine;
using UnityEngine.Events;

namespace LuciferGamingStudio
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        private PlayerInputActions playerInputActions;

        #region Unity Events

        public UnityEvent OnJumpPressed;
        public UnityEvent OnSprintPressed;
        public UnityEvent OnSprintReleased;
        public UnityEvent OnInteractPressed;
        public UnityEvent OnInteractReleased;

        #endregion

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            playerInputActions.Enable();

            playerInputActions.PlayerController.Jump.performed += ctx => OnJumpPressed?.Invoke();
            playerInputActions.PlayerController.Sprint.performed += ctx => OnSprintPressed?.Invoke();
            playerInputActions.PlayerController.Sprint.canceled += ctx => OnSprintReleased?.Invoke();
            playerInputActions.Tools.Interact.performed += ctx => OnInteractPressed?.Invoke();
            playerInputActions.Tools.Interact.canceled += ctx => OnInteractReleased?.Invoke();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();

            playerInputActions.PlayerController.Jump.performed -= ctx => OnJumpPressed?.Invoke();
            playerInputActions.PlayerController.Sprint.performed -= ctx => OnSprintPressed?.Invoke();
            playerInputActions.PlayerController.Sprint.canceled -= ctx => OnSprintReleased?.Invoke();
            playerInputActions.Tools.Interact.performed -= ctx => OnInteractPressed?.Invoke();
            playerInputActions.Tools.Interact.canceled -= ctx => OnInteractReleased?.Invoke();
        }

        public Vector2 GetMovementInput()
        {
            return playerInputActions.PlayerController.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetLookInput()
        {
            return playerInputActions.PlayerController.Look.ReadValue<Vector2>();
        }

        public bool IsJumpPressedInThisFrame()
        {
            return playerInputActions.PlayerController.Jump.triggered;
        }

        public bool IsSprintPressed()
        {
            return playerInputActions.PlayerController.Sprint.IsPressed();
        }
    }
}
