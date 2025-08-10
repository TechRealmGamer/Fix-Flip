using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LuciferGamingStudio
{
    public class ToolsSelector : MonoBehaviour
    {
        [SerializeField]
        private float mouseDeltaThreshold = 3f;
        [SerializeField]
        private RectTransform toolsMenu;
        [SerializeField]
        private Image[] toolsLogo;
        [SerializeField]
        private Tool[] tools;

        private int currentToolIndex = 0;
        private bool isMenuVisible = false;

        private void OnEnable()
        {
            InputManager.Instance.OnChangeToolPressed.AddListener(ShowToolsMenu);
            InputManager.Instance.OnChangeToolReleased.AddListener(HideToolsMenu);
        }

        private void Update()
        {
            if (!isMenuVisible)
                return;

            Vector2 mouseDelta = InputManager.Instance.GetLookInput();

            if (mouseDelta.sqrMagnitude > mouseDeltaThreshold)
            {
                int region = GetRegion(mouseDelta);
                if (region != currentToolIndex)
                {
                    toolsLogo[currentToolIndex].DOColor(Color.gray8, 0.2f);
                    currentToolIndex = region;
                    toolsLogo[currentToolIndex].DOColor(Color.orange, 0.2f);
                }
            }
        }

        public void ShowToolsMenu()
        {
            toolsMenu.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            isMenuVisible = true;
        }

        public void HideToolsMenu()
        {
            isMenuVisible = false;

            toolsMenu.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

            // Disable all tools except the current one
            foreach (Tool tool in tools)
                tool.enabled = false;
            tools[currentToolIndex].enabled = true;
        }

        int GetRegion(Vector2 v)
        {
            // Step 1: Calculate angle (in degrees) from x-axis
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            // Normalize to 0–360
            if (angle < 0)
                angle += 360f;

            // Step 3: Get region index (0 to 3)
            int regionIndex = Mathf.FloorToInt(angle / 90f);

            return regionIndex;
        }
    }
}
