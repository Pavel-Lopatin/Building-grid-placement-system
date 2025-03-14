using UnityEngine;

namespace BuldingSystem
{
    public class GuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingPanel;

        public void Init()
        {
            Show();
            Debug.Log($"{name} initialized");
        }

        public void Show()
        {
            _buildingPanel.SetActive(true);
        }

        private void Hide()
        {
            _buildingPanel.SetActive(false);
        }
    }

}
