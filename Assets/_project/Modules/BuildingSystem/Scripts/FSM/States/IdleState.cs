using UnityEngine;

namespace BuildingSystem
{
    public class IdleState : FsmState
    {
        public IdleState(Fsm fsm, BuildingSystemController controller, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, PositionCalculator positionCalculator, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, controller, buildingPlacer, buildingPreview, guiController, inputController, grid, positionCalculator, buildingDataBase, gridData)
        {
            _guiController.OnBuldingSelectionButtonClicked += BuildingSelected;
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Select building");
            _guiController.SetActiveToBuildButton(false);
            _guiController.OnDeleteButtonClicked += TransitionToDeletionState;
        }

        private void BuildingSelected(int id)
        {
            _lastID = id;
            _buildingPreview.PrepareBuildingPrefab(_buildingsDataBase.buildings[_lastID].Prefab);
            _fsm.SetState<ConstructionState>();
        }

        private void TransitionToDeletionState()
        {
            _fsm.SetState<DelectionState>();
        }

        public override void Exit()
        {
            _guiController.OnDeleteButtonClicked -= TransitionToDeletionState;
            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}