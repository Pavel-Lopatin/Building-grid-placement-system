using UnityEngine;

namespace BuildingSystem
{
    public class ConstructionState : FsmState
    {
        public ConstructionState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, BuildingDataBase buildingDataBase, Grid grid) : base(fsm, buildingPlacer, buildingPreview, guiController, inputController, buildingDataBase, grid)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Press BUILD button");
            _guiController.OnDeleteButtonClicked += TransitionToDelectionState;
            _guiController.OnBuldingSelectionButtonClicked += BuildingButtonIdSelected;
            _guiController.OnBuildButtonClicked += BuildButtonClicked;
            _inputController.OnEscapeButtonClicked += ForcedTransitionToIdleState;
        }

        private void BuildingButtonIdSelected(int id)
        {
            _guiController.UpdateStateText("Press BUILD button");
            _buildingPreview.PrepareBuildingPrefab(_buildingsDataBase.buildings[id].Prefab);
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
            Vector3Int position = _grid.WorldToCell(_buildingPreview.CalculatePositionOnPlane(_inputController.ReadMousePosition()));
            _buildingPreview.Show(position);
        }

        public override void Update()
        {
            if (_inputController.IsCursorOverUI() || !_buildingPreview.IsReadyToShow())
                return;

            Vector3Int position = _grid.WorldToCell(_buildingPreview.CalculatePositionOnPlane(_inputController.ReadMousePosition()));
            _buildingPreview.UpdatePosition(position);
        }

        private void TransitionToDelectionState()
        {
            _fsm.SetState<DelectionState>();
        }

        private void ForcedTransitionToIdleState()
        {
            _fsm.SetState<IdleState>();
        }

        public override void Exit()
        {
            _buildingPreview.Hide();

            _guiController.OnBuldingSelectionButtonClicked -= BuildingButtonIdSelected;
            _guiController.OnDeleteButtonClicked -= TransitionToDelectionState;
            _guiController.OnBuildButtonClicked -= BuildButtonClicked;
            _inputController.OnEscapeButtonClicked -= ForcedTransitionToIdleState;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}