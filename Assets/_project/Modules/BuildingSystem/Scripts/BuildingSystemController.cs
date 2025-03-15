using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSystemController : MonoBehaviour
    {
        [Tooltip("Data")]
        [SerializeField] private BuildingDataBase _buildingsDataBase;
        [SerializeField] private LayerMask _buildingLayerMask;
        private GridData _gridData;

        [Tooltip("Services")]
        [SerializeField] private BuildingPlacer _buildingPlacer;
        [SerializeField] private BuildingPreview _buildingPreview;
        [SerializeField] private GuiController _guiController;
        [SerializeField] private InputController _inputController;
        [SerializeField] private Grid _grid;
        [SerializeField] private PositionCalculator _positionCalculator;

        [Tooltip("Components")]
        Camera _camera;
        private Fsm _fsm;

        public event Action<PlacementData> OnGridDataUpdated;
        public event Action<int> OnBuildingRemoved;


        public void Init(List<PlacementData> loadedData)
        {
            _camera = Camera.main;

            InitializeServices();
            InitializeStates();

            if (loadedData != null)
            {
                foreach (var data in loadedData)
                {
                    Build(data.OccupiedPosition, data.ID, data.BuildIndex);
                }
            }
        }

        private void InitializeServices()
        {
            _gridData = new GridData();

            _buildingPlacer.Init();
            _buildingPreview.Init();
            _guiController.Init();
            _inputController.Init();
            _positionCalculator.Init(_camera, _buildingLayerMask);
        }

        private void InitializeStates()
        {
            _fsm = new Fsm();
            Debug.Log($"State machine for {name} initialized");

            _fsm.AddState(new IdleState(_fsm, this, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase, _gridData));
            _fsm.AddState(new ConstructionState(_fsm, this, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase, _gridData));
            _fsm.AddState(new DelectionState(_fsm, this, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase, _gridData));

            _fsm.SetState<IdleState>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        public void GridDataUpdate(PlacementData newData)
        {
            OnGridDataUpdated?.Invoke(newData);
        }

        public void BuildingRemoved(int index)
        {
            OnBuildingRemoved?.Invoke(index);
        }

        private void Build(Vector3Int position, int id, int buildIndex)
        {
            _buildingPlacer.PlaceBuildingFromSave(_buildingsDataBase.buildings[id].Prefab, position);
            _gridData.AddObject(position, id, buildIndex);
        }
    }
}