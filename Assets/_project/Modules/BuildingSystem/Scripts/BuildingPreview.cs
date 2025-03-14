using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private GameObject _currentPreviewObject;

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

        public void Show(GameObject prefab, Vector3Int position)
        {
            GameObject newPreview = Instantiate(prefab, position, Quaternion.identity);
            _currentPreviewObject = newPreview;

            ChangePreviewTransparency();
        }

        private void ChangePreviewTransparency()
        {
            Renderer[] renderers = _currentPreviewObject.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                Material[] materials = renderers[i].materials;

                for (int m = 0; m < materials.Length; m++)
                {
                    Color transparencyColor = new Color(materials[m].color.r, materials[m].color.g, materials[m].color.b, 0.5f);
                    renderers[i].materials[m].color = transparencyColor;
                }
            }
        }

        public void UpdatePosition(Vector3Int position)
        {
            _currentPreviewObject.transform.position = position + _offsetPosition;
        }

        public void Hide()
        {
            Destroy(_currentPreviewObject.gameObject);
            _currentPreviewObject = null;
        }
    }
}

