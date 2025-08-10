using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace LuciferGamingStudio
{
    public class CleaningBrush : Tool
    {
        public float cleaningRate = 0.1f; // Adjust the rate of cleaning
        
        private bool isCleaning = false;

        private void Update()
        {
            if (!isCleaning)
                return;
            
            Interactable interactable = InteractionManager.Instance.GetObjectInFocus();

            if (interactable != null && interactable.CompareTag("Dirt"))
            {
                Renderer renderer = interactable.GetComponent<Renderer>();
                float alpha = renderer.material.color.a;
                
                alpha -= Time.deltaTime * cleaningRate;
                renderer.material.DOFade(alpha, 0.1f);

                if (alpha <= 0f)
                {
                    Destroy(interactable.gameObject); // Destroy the dirt object when fully cleaned
                }
            }
            else
            {
                Debug.Log("No interactable object in focus.");
            }
        }

        public override void Interact()
        {
            isCleaning = true;
        }

        public override void StopInteract()
        {
            isCleaning = false;
        }
    }
}
