using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private GameObject _currentPreviewObject;
        [SerializeField] private GameObject _currentBuildingPrefab;

        private Camera _sceneCamera;
        private LayerMask _placementLayerMask;
        private Vector3 _lastPosition;
        private Vector3 _offsetPosition;

        public void Init(Camera camera, LayerMask layerMask)
        {
            _offsetPosition = new Vector3(0, _offsetY, 0);

            _sceneCamera = camera;
            _placementLayerMask = layerMask;
        }

        public Vector3 CalculatePositionOnPlane(Vector2 mousePosition)
        {
            Vector3 newMousePosition = new Vector3(mousePosition.x, mousePosition.y, _sceneCamera.nearClipPlane);
            Ray ray = _sceneCamera.ScreenPointToRay(newMousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, _placementLayerMask))
                _lastPosition = hit.point;

            return _lastPosition;
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
                    Color transparencyColor = new Color(materials[m].color.r, materials[m].color.g, materials[m].color.b, 0.1f);
                    renderers[i].sharedMaterials[m].color = transparencyColor;
                }
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

