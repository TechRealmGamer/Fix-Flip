using UnityEngine;

namespace LuciferGamingStudio
{
    public class Tool : MonoBehaviour
    {
        [SerializeField]
        private float interactionDistance = 3f;

        private void OnEnable()
        {
            InputManager.Instance.OnInteractPressed.AddListener(Interact);
        }

        private void OnDisable()
        {
            InputManager.Instance.OnInteractPressed.RemoveListener(Interact);
        }

        public virtual void Interact()
        {
            Interactable interactable = GetObjectInFocus();
            
            if (interactable != null)
            {
                // Perform interaction logic here
                Debug.Log("Interacted with: " + interactable.name);
            }
            else
            {
                Debug.Log("No interactable object in focus.");
            }
        }

        public Interactable GetObjectInFocus()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                    return interactable;
            }
            return null;
        }
    }
}
