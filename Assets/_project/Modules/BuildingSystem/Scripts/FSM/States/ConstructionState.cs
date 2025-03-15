using UnityEngine;

namespace BuildingSystem
{
    public class ConstructionState : FsmState
    {
        private int _id;

        public ConstructionState(Fsm fsm, BuildingSystemController controller, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, PositionCalculator positionCalculator, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, controller, buildingPlacer, buildingPreview, guiController, inputController, grid, positionCalculator, buildingDataBase, gridData)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Press BUILD button");
            _guiController.SetActiveToBuildButton(true);
            _guiController.OnDeleteButtonClicked += TransitionToDelectionState;
            _guiController.OnBuldingSelectionButtonClicked += BuildingButtonIdSelected;
            _guiController.OnBuildButtonClicked += BuildButtonClicked;
            _inputController.OnEscapeButtonClicked += TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked += TryToBuild;

            _id = _lastID;
        }

        private void BuildingButtonIdSelected(int id)
        {
            _id = id;
            _guiController.UpdateStateText("Press BUILD button");
            _buildingPreview.PrepareBuildingPrefab(_buildingsDataBase.buildings[_id].Prefab);
        }

        private void BuildButtonClicked()
        {
            if (_buildingPreview.IsReadyToShow())
            {
                ShowBuildingPreview();
                _guiController.UpdateStateText("Select a construction PLACE");
            }
        }

        private void ShowBuildingPreview()
        {
            _buildingPreview.Show(CalculatePosition());
        }

        private void TryToBuild()
        {
            if (_inputController.IsCursorOverUI() || !_buildingPreview.IsReadyToShow())
                return;

            if (!_gridData.IsContainBuilding(CalculatePosition()))
            {
                Build();
            }
        }

        private void Build()
        {
            int buildIndex = _buildingPlacer.PlaceBuilding(_buildingsDataBase.buildings[_id].Prefab, CalculatePosition());
            _controller.GridDataUpdate(_gridData.AddObject(CalculatePosition(), _id, buildIndex)); 
        }

        public override void Update()
        {
            if (_inputController.IsCursorOverUI() || !_buildingPreview.IsReadyToShow())
                return;

            _buildingPreview.UpdatePosition(CalculatePosition());
        }

        private Vector3Int CalculatePosition()
        {
            Vector3Int position = _grid.WorldToCell(_positionCalculator.CalculatePositionOnPlane(_inputController.ReadMousePosition()));
            return position;
        }

        private void TransitionToDelectionState()
        {
            _fsm.SetState<DelectionState>();
        }

        private void TransitionToIdleState()
        {
            _fsm.SetState<IdleState>();
        }

        public override void Exit()
        {
            _buildingPreview.ClearPreview();

            _guiController.OnBuldingSelectionButtonClicked -= BuildingButtonIdSelected;
            _guiController.OnDeleteButtonClicked -= TransitionToDelectionState;
            _guiController.OnBuildButtonClicked -= BuildButtonClicked;
            _inputController.OnEscapeButtonClicked -= TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked -= TryToBuild;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}