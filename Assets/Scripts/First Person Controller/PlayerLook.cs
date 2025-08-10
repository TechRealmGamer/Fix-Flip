using UnityEngine;
using Unity.Cinemachine;

namespace LuciferGamingStudio
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField]
        private float mouseXSensitivity = 100f;
        [SerializeField]
        private float mouseYSensitivity = 100f;
        [SerializeField]
        private float maxLookAngle = 90f;

        private Transform camTransform;
        private Vector3 initialCamRotation;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            camTransform = GetComponentInChildren<CinemachineCamera>().transform;
            if (camTransform == null)
            {
                Debug.LogError("No camera found in the scene. Please ensure a camera is present.");
            }
        }

        private void LateUpdate()
        {
            if (InputManager.Instance.IsChangeToolPressed())
                return; // Skip camera movement when changing tools

            PerformCameraMovement();
        }

        private void PerformCameraMovement()
        {
            // If Cursor is not locked, do not process look input
            if (Cursor.lockState != CursorLockMode.Locked)
                return;

            // Get mouse input
            Vector2 lookInput = InputManager.Instance.GetLookInput();

            if (lookInput == Vector2.zero)
                return;

            // Rotate the player and camera based on mouse input
            transform.Rotate(Vector3.up * lookInput.x * mouseXSensitivity * Time.deltaTime);

            initialCamRotation.y += lookInput.y * mouseYSensitivity * Time.deltaTime;
            initialCamRotation.y = Mathf.Clamp(initialCamRotation.y, -maxLookAngle, maxLookAngle);

            camTransform.localEulerAngles = new Vector3(-initialCamRotation.y, 0f, 0f);
        }
    }
}
