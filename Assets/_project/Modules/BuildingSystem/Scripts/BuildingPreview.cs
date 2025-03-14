using UnityEngine;

namespace BuldingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private GameObject _currentPreviewObject;

        private Vector3 _offsetPosition;

        public void Init()
        {
            _offsetPosition = new Vector3(0, _offsetY, 0);
        }

        public void ShowObjectPreview(GameObject prefab, Vector3Int position)
        {
            GameObject newPreview = Instantiate(prefab, position, Quaternion.identity);
            _currentPreviewObject = newPreview;
        }

        private void UpdatePreviewPosition(Vector3Int position)
        {
            _currentPreviewObject.transform.position = position + _offsetPosition;
        }

        public void HideObjectPreview()
        {
            Destroy(_currentPreviewObject.gameObject);
            _currentPreviewObject = null;
        }
    }
}

