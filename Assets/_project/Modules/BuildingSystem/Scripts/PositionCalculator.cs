using UnityEngine;

namespace BuildingSystem
{
    public class PositionCalculator : MonoBehaviour
    {
        private Camera _sceneCamera;
        private LayerMask _placementLayerMask;
        private Vector3 _lastPosition;

        public void Init(Camera camera, LayerMask layerMask)
        {
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
    }
}