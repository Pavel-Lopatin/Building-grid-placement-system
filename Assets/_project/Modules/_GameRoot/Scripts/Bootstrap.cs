using UnityEngine;
using BuildingSystem;

namespace GameRoot
{
    public class Bootstrap : MonoBehaviour
    {
        private BuildingSystemController _buildingSystemController;

        private void Awake()
        {
            Debug.Log("Game loaded");
            InitServices();
        }

        private void InitServices()
        {
            var buildingSystem = Resources.Load<BuildingSystemController>("BuildingSystem");
            buildingSystem = Instantiate(buildingSystem);
            _buildingSystemController = buildingSystem;
            Debug.Log($"{buildingSystem.name} added to scene");

            // TODO
            // save system

            StartGameplay();
        }

        private void StartGameplay()
        {
            _buildingSystemController.Init();
            Debug.Log("Gameplay started!");
        }

    }
}