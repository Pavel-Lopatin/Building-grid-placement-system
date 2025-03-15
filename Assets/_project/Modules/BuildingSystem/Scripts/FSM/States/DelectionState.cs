using UnityEngine;

namespace BuildingSystem
{
    public class DelectionState : FsmState
    {
        public DelectionState(Fsm fsm, BuildingSystemController controller, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, PositionCalculator positionCalculator, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, controller, buildingPlacer, buildingPreview, guiController, inputController, grid, positionCalculator, buildingDataBase, gridData)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.SetActiveToBuildingsButtons(false);
            _guiController.UpdateStateText("Demolition mode. ESC to quit");
            _guiController.OnDeleteButtonClicked += TransitionToIdleState;
            _guiController.OnBuildButtonClicked += TransitionToIdleState;
            _inputController.OnEscapeButtonClicked += TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked += TryToDestroy;
        }

        private void TryToDestroy()
        {
            if (_inputController.IsCursorOverUI())
                return;

            if (!_gridData.IsContainBuilding(CalculatePosition()))
                return;

            int destroyIndex = _gridData.Remove(CalculatePosition());

            _buildingPlacer.DestroyBuilding(destroyIndex);
            _controller.BuildingRemoved(destroyIndex);
        }

        private Vector3Int CalculatePosition()
        {
            Vector3Int position = _grid.WorldToCell(_positionCalculator.CalculatePositionOnPlane(_inputController.ReadMousePosition()));
            return position;
        }


        private void TransitionToIdleState()
        {
            _fsm.SetState<IdleState>();
        }

        public override void Exit()
        {
            _guiController.SetActiveToBuildingsButtons(true);
            _guiController.OnDeleteButtonClicked -= TransitionToIdleState;
            _inputController.OnEscapeButtonClicked -= TransitionToIdleState;
            _inputController.OnLeftMouseButtonClicked -= TryToDestroy;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}