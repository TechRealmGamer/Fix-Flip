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

        public virtual void Interact() {}
    }
}
