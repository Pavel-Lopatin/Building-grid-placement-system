using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private GameObject _currentPreviewObject;
        [SerializeField] private GameObject _currentBuildingPrefab;
        [SerializeField] private Material _previewMaterial;

        private Camera _sceneCamera;
        private LayerMask _placementLayerMask;
        private Vector3 _lastPosition;
        private Vector3 _offsetPosition;

        public void Init()
        {
            _offsetPosition = new Vector3(0, _offsetY, 0);
        }

        public void PrepareBuildingPrefab(GameObject prefab)
        {
            Hide();

            _currentBuildingPrefab = Instantiate(prefab);
            _currentBuildingPrefab.SetActive(false);
            ChangePreviewTransparency();
        }

        private void ChangePreviewTransparency()
        {
            Renderer[] renderers = _currentBuildingPrefab.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                Material[] materials = renderers[i].sharedMaterials;

                for (int m = 0; m < materials.Length; m++)
                {
                    materials[m] = _previewMaterial;
                }

                renderers[i].sharedMaterial = _previewMaterial;
            }
        }

        public bool IsReadyToShow()
        {
            if (_currentBuildingPrefab != null) return true;
            return false;
        }

        public void Show(Vector3Int position)
        {
            _currentPreviewObject = _currentBuildingPrefab;
            _currentPreviewObject.SetActive(true);
        }

        public void UpdatePosition(Vector3Int position)
        {
            if (!_currentPreviewObject)
                return;

            _currentPreviewObject.transform.position = position + _offsetPosition;
        }

        public void Hide()
        {
            Destroy(_currentBuildingPrefab.gameObject);
            _currentBuildingPrefab = null;
            _currentPreviewObject = null;
        }
    }
}

