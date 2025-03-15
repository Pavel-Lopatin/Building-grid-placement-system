using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _placedGameObjects;

        public void Init()
        {
            _placedGameObjects = new List<GameObject>();
        }

        public int PlaceBuilding(GameObject prefab, Vector3Int position)
        {
            var newBuilding = Instantiate(prefab, position, Quaternion.identity);
            _placedGameObjects.Add(newBuilding);
            Debug.Log($"The building {prefab.name} is built!");

            int index = _placedGameObjects.LastIndexOf(newBuilding);
            return index;
        }

        public void PlaceBuildingFromSave(GameObject prefab, Vector3Int position)
        {
            var newBuilding = Instantiate(prefab, position, Quaternion.identity);
            _placedGameObjects.Add(newBuilding);
            Debug.Log($"The building {prefab.name} is built from save!");
        }

        public void DestroyBuilding(int buildingIndex)
        {
            Destroy(_placedGameObjects[buildingIndex]);
            _placedGameObjects[buildingIndex] = null;

            Debug.Log("The building is destroyed!");
        }
    }
}