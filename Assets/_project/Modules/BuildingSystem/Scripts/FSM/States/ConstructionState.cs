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
            _inputController.OnEscapeButtonClicked += ForcedTransitionToIdleState;

            ShowBuildingPreview();
        }

        private void ShowBuildingPreview()
        {
            
            _buildingPreview.Show(_buildingsDataBase.buildings[_lastBuildingID].Prefab, _grid.WorldToCell(_buildingPreview.CalculatePositionOnPlane(_inputController.ReadMousePosition())));
        }

        public override void Update()
        {
            _buildingPreview.UpdatePosition(_grid.WorldToCell(_buildingPreview.CalculatePositionOnPlane(_inputController.ReadMousePosition())));
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

            _guiController.OnDeleteButtonClicked -= TransitionToDelectionState;
            _inputController.OnEscapeButtonClicked -= ForcedTransitionToIdleState;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}