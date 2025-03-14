using UnityEngine;
using UnityEngine.UI;

namespace BuildingSystem
{
    public class GuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingPanel;
        [SerializeField] private Text _buildingStateText;

        public void Init()
        {
            Show();
            Debug.Log($"{name} initialized");
        }

        public void Show()
        {
            _buildingPanel.SetActive(true);
        }

        public void Hide()
        {
            _buildingPanel.SetActive(false);
        }

        public void UpdateStateText(string newText)
        {
            _buildingStateText.text = newText;
        }
    }
}