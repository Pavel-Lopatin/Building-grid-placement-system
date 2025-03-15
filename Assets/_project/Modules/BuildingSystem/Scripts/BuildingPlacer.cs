using System.Collections.Generic;
using System.Linq;
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
            Debug.Log("The building is built!");

            int index = _placedGameObjects.LastIndexOf(newBuilding);
            return index;
        }

        public void DestroyBuilding(int buildingIndex)
        {
            Destroy(_placedGameObjects[buildingIndex]);
            _placedGameObjects[buildingIndex] = null;

            Debug.Log("The building is destroyed!");
        }
    }
}