using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveSystemController : MonoBehaviour
    {
        public SerializableList<Building> BuildingsList => _buildingsList;
        [SerializeField] private SerializableList<Building> _buildingsList = new SerializableList<Building>();

        private string _filename = "buildings.json";
        public string FileName => _filename;

        public void RemovePlacementData(int index)
        {
            foreach (Building b in _buildingsList.list.ToList())
            {
                if (b.BuildIndex == index)
                {
                    _buildingsList.list.Remove(b);
                    Save(_filename);
                }
            }
        }

        public void SetNewPlacementData(Vector3Int vector3, int ID, int buildIndex)
        {
            Building data = new Building(vector3.x, vector3.y, vector3.z, ID, buildIndex);
            _buildingsList.list.Add(data);
            Save(_filename);
        }

        public void Save(string fileName)
        {
            string json = JsonUtility.ToJson(_buildingsList, true);
            string path = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllText(path, json);
            Debug.Log($"Saved buildings to {path}");
        }

        public void Load(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _buildingsList = JsonUtility.FromJson<SerializableList<Building>>(json);
                Debug.Log($"Loaded buildings from {path}");
            }
            else
            {
                Save(_filename);
            }
        }
    }

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> list;
    }

    [System.Serializable]
    public struct Building
    {
        public int X;
        public int Y;
        public int Z;
        public int ID;
        public int BuildIndex;

        public Building(int x, int y, int z, int id, int buildIndex)
        {
            X = x;
            Y = y;
            Z = z;
            ID = id;
            BuildIndex = buildIndex;
        }
    }
}