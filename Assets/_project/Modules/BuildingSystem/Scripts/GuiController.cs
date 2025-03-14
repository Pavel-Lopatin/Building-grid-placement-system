using System;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingSystem
{
    public class GuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingPanel;
        [SerializeField] private Text _buildingStateText;

        public event Action OnBuildButtonClicked;
        public event Action OnDeleteButtonClicked;
        public event Action<int> OnBuldingSelectionButtonClicked;

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

        public void BuildButtonClicked()
        {
            OnBuildButtonClicked?.Invoke();
        }

        public void DeleteButtonClicked()
        {
            OnDeleteButtonClicked?.Invoke();
        }

        public void BuldingSelectionButtonClicked(int id)
        {
            OnBuldingSelectionButtonClicked?.Invoke(id);
        }

    }
}