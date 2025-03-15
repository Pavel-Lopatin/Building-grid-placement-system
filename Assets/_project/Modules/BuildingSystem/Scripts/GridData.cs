using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class GridData
    {
        private Dictionary<Vector3Int, PlacementData> _placedObjects = new Dictionary<Vector3Int, PlacementData>();

        public void AddObject(Vector3Int gridPosition, int ID, int buildIndex)
        {
            PlacementData data = new PlacementData(gridPosition, ID, buildIndex);
            _placedObjects.Add(gridPosition, data);
        }

        public bool IsContainBuilding(Vector3Int gridPosition)
        {
            if (_placedObjects.ContainsKey(gridPosition))
                return false;

            return true;
        }

        public int Remove(Vector3Int gridPosition)
        {
            int index = _placedObjects[gridPosition].BuildIndex;
            _placedObjects.Remove(gridPosition);
            return index;
        }
    }

    public class PlacementData
    {
        public Vector3Int OccupiedPosition { get; private set; }
        public int ID { get; private set; }
        public int BuildIndex { get; private set; }

        public PlacementData(Vector3Int occupiedPosition, int id, int buildIndex)
        {
            OccupiedPosition = occupiedPosition;
            ID = id;
            BuildIndex = buildIndex;
        }
    }
}
