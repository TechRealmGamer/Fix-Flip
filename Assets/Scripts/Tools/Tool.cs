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
            InputManager.Instance.OnInteractReleased.AddListener(StopInteract);
        }

        private void OnDisable()
        {
            InputManager.Instance.OnInteractPressed.RemoveListener(Interact);
            InputManager.Instance.OnInteractReleased.RemoveListener(StopInteract);
        }

        public virtual void Interact() {}
        public virtual void StopInteract() { }
    }
}
