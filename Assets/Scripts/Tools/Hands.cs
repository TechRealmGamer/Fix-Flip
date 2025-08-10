using UnityEngine;

namespace LuciferGamingStudio
{
    public class Hands : Tool
    {
        public override void Interact()
        {
            Interactable interactable = GetObjectInFocus();
            
            if (interactable != null)
            {
                // Perform interaction logic here
                if(interactable.CompareTag("Trash"))
                    Destroy(interactable.gameObject);

                Debug.Log("Interacted with: " + interactable.name);
            }
            else
            {
                Debug.Log("No interactable object in focus.");
            }
        }
    }
}
