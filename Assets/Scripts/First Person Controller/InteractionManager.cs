using Unity.Cinemachine;
using UnityEngine;

namespace LuciferGamingStudio
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance { get; private set; }

        [SerializeField]
        private float interactionDistance = 3f;

        private Transform firstPersonCam;
        private GameObject currentFocusedObject;
        private bool canInteract = true;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            firstPersonCam = GetComponentInChildren<CinemachineCamera>().transform;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, out hit, interactionDistance))
            {
                GameObject newObjectInFocus = hit.collider.gameObject;
                
                if (newObjectInFocus == currentFocusedObject)
                    return;

                if (currentFocusedObject != null)
                    currentFocusedObject.layer = LayerMask.NameToLayer("Default");

                if (newObjectInFocus.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    currentFocusedObject = newObjectInFocus;
                    currentFocusedObject.layer = LayerMask.NameToLayer("Highlighted");
                }
                else
                {
                    currentFocusedObject = null;
                }
            }
            else
            {
                if (currentFocusedObject != null)
                {
                    currentFocusedObject.layer = LayerMask.NameToLayer("Default");
                    currentFocusedObject = null;
                }
            }
        }

        public Interactable GetObjectInFocus()
        {
            if (currentFocusedObject == null)
                return null;
            else if (currentFocusedObject.TryGetComponent<Interactable>(out Interactable interactable))
                return interactable;
            else
                return null;
        }
    }
}
