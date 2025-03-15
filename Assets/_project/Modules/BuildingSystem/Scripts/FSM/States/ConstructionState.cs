using UnityEngine;

namespace BuildingSystem
{
    public class ConstructionState : FsmState
    {
        private int _id;

        public ConstructionState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, buildingPlacer, buildingPreview, guiController, inputController, grid, buildingDataBase, gridData)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Press BUILD button");
            _guiController.OnDeleteButtonClicked += TransitionToDelectionState;
            _guiController.OnBuldingSelectionButtonClicked += BuildingButtonIdSelected;
            _guiController.OnBuildButtonClicked += BuildButtonClicked;
            _inputController.OnEscapeButtonClicked += TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked += TryToBuild;
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

            if (_gridData.CanPlace(CalculatePosition()))
            {
                _buildingPlacer.PlaceBuilding(_buildingsDataBase.buildings[_id].Prefab, CalculatePosition());
                _gridData.AddObject(CalculatePosition(), _id);
            }
        }

        public override void Update()
        {
            if (_inputController.IsCursorOverUI() || !_buildingPreview.IsReadyToShow())
                return;

            _buildingPreview.UpdatePosition(CalculatePosition());
        }

        private Vector3Int CalculatePosition()
        {
            Vector3Int position = _grid.WorldToCell(_buildingPreview.CalculatePositionOnPlane(_inputController.ReadMousePosition()));
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
            _buildingPreview.Hide();

            _guiController.OnBuldingSelectionButtonClicked -= BuildingButtonIdSelected;
            _guiController.OnDeleteButtonClicked -= TransitionToDelectionState;
            _guiController.OnBuildButtonClicked -= BuildButtonClicked;
            _inputController.OnEscapeButtonClicked -= TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked -= TryToBuild;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}