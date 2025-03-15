using UnityEngine;
using BuildingSystem;
using SaveLoadSystem;
using System.Collections.Generic;

namespace GameRoot
{
    public class Bootstrap : MonoBehaviour
    {
        private BuildingSystemController _buildingSystemController;
        private SaveSystemController _saveSystemContoller;

        private void Awake()
        {
            Debug.Log("Game loaded");
            InitServices();
        }

        private void InitServices()
        {
            var buildingSystem = Resources.Load<BuildingSystemController>("BuildingSystem");
            _buildingSystemController = Instantiate(buildingSystem);
            Debug.Log($"{_buildingSystemController.name} added to scene");

            var saveSystem = Resources.Load<SaveSystemController>("SaveSystem");
            _saveSystemContoller = Instantiate(saveSystem);
            Debug.Log($"{_saveSystemContoller.name} added to scene");

            SubscribeToEvents();
            StartGameplay();
        }

        private void SubscribeToEvents()
        {
            _buildingSystemController.OnGridDataUpdated += Save;
            _buildingSystemController.OnBuildingRemoved += RemoveBuildingFromList;
        }

        private void StartGameplay()
        {
            _buildingSystemController.Init(Load());
            Debug.Log("Gameplay started!");
        }

        private List<PlacementData> Load()
        {
            _saveSystemContoller.Load(_saveSystemContoller.FileName);
            SerializableList<Building> actualData = _saveSystemContoller.BuildingsList;

            if (actualData.list.Count == 0) return null;

            List<PlacementData> loadedData = new List<PlacementData>();

            for (int i = 0; i < actualData.list.Count; ++i)
            {
                Vector3Int position = new Vector3Int(actualData.list[i].X, actualData.list[i].Y, actualData.list[i].Z);
                int id = actualData.list[i].ID;
                int buildIndex = actualData.list[i].BuildIndex;

                loadedData.Add(new PlacementData(position, id, buildIndex));
            }
            return loadedData;
        }

        private void RemoveBuildingFromList(int index)
        {
            _saveSystemContoller.RemovePlacementData(index);
        }

        private void Save(PlacementData newData)
        {
            Vector3Int vector = newData.OccupiedPosition;
            int ID = newData.ID;
            int buildIndex = newData.BuildIndex;
            _saveSystemContoller.SetNewPlacementData(vector, ID, buildIndex);
        }
    }
}