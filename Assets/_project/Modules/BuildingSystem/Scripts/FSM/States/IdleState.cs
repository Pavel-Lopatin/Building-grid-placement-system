using UnityEngine;

namespace BuildingSystem
{
    public class IdleState : FsmState
    {
        public IdleState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, PositionCalculator positionCalculator, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, buildingPlacer, buildingPreview, guiController, inputController, grid, positionCalculator, buildingDataBase, gridData)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Select building");
            _guiController.SetActiveToBuildButton(false);
            _guiController.OnBuldingSelectionButtonClicked += BuildingSelected;
            _guiController.OnDeleteButtonClicked += TransitionToDeletionState;
        }

        private void BuildingSelected(int id)
        {
            _buildingPreview.PrepareBuildingPrefab(_buildingsDataBase.buildings[id].Prefab);
            _fsm.SetState<ConstructionState>();
        }

        private void TransitionToDeletionState()
        {
            _fsm.SetState<DelectionState>();
        }

        public override void Exit()
        {
            _guiController.OnBuldingSelectionButtonClicked -= BuildingSelected;
            _guiController.OnDeleteButtonClicked -= TransitionToDeletionState;
            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}