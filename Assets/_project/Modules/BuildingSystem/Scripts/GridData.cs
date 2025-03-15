using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class GridData
    {
        private Dictionary<Vector3Int, PlacementData> _placedObjects = new Dictionary<Vector3Int, PlacementData>();

        public void AddObject(Vector3Int gridPosition, int ID)
        {
            PlacementData data = new PlacementData(gridPosition, ID);
            _placedObjects.Add(gridPosition, data);
        }

        public bool CanPlace(Vector3Int gridPosition)
        {
            if (_placedObjects.ContainsKey(gridPosition))
                return false;

            return true;
        }

        public void Remove(Vector3Int gridPosition)
        {
            _placedObjects.Remove(gridPosition);
        }
    }

    public class PlacementData
    {
        public Vector3Int OccupiedPosition { get; private set; }
        public int ID { get; private set; }

        public PlacementData(Vector3Int occupiedPosition, int id)
        {
            OccupiedPosition = occupiedPosition;
            ID = id;
        }
    }
}
