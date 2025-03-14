using UnityEngine;

namespace BuildingSystem
{
    public class IdleState : FsmState
    {
        public IdleState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, BuildingDataBase buildingDataBase, Grid grid) : base(fsm, buildingPlacer, buildingPreview, guiController, inputController, buildingDataBase, grid)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Select a building to build");
            _guiController.OnBuldingSelectionButtonClicked += OnBuildingSelected;
            _guiController.OnDeleteButtonClicked += TransitionToDeletionState;
        }

        private void OnBuildingSelected(int id)
        {
            Debug.Log($"{id}");
            _lastBuildingID = id;
            _fsm.SetState<ConstructionState>();
        }

        private void TransitionToDeletionState()
        {
            _fsm.SetState<DelectionState>();
        }

        public override void Exit()
        {
            _guiController.OnBuldingSelectionButtonClicked -= OnBuildingSelected;
            _guiController.OnDeleteButtonClicked -= TransitionToDeletionState;
            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }
}