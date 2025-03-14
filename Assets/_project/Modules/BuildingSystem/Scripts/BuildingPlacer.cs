using System.Collections.Generic;
using UnityEngine;

namespace BuldingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _placedGameObjects;

        public void Init()
        {
            _placedGameObjects = new List<GameObject>();
        }

        public void PlaceBuilding(GameObject prefab, Vector3Int position)
        {
            var newBuilding = Instantiate(prefab, position, Quaternion.identity);
            _placedGameObjects.Add(newBuilding);

            Debug.Log("The building is built!");
        }

        public void DestroyBuilding(int buildingIndex)
        {
            Destroy(_placedGameObjects[buildingIndex]);
            _placedGameObjects[buildingIndex] = null;
        }
    }
}