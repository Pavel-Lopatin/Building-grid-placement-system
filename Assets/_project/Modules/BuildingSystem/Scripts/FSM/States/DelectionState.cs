using UnityEngine;

namespace BuildingSystem
{
    public class DelectionState : FsmState
    {
        public DelectionState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, BuildingDataBase buildingDataBase, GridData gridData) : base(fsm, buildingPlacer, buildingPreview, guiController, inputController, grid, buildingDataBase, gridData)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{GetType().Name} ENTERED");

            _guiController.UpdateStateText("Demolition mode");
            _guiController.OnDeleteButtonClicked += TransitionToIdleState;
            _inputController.OnEscapeButtonClicked += TransitionToIdleState;
        }

        private void TransitionToIdleState()
        {
            _fsm.SetState<IdleState>();
        }

        public override void Exit()
        {
            _guiController.OnDeleteButtonClicked -= TransitionToIdleState;
            _inputController.OnEscapeButtonClicked -= TransitionToIdleState;

            Debug.Log($"{GetType().Name} COMPLETED");
        }
    }

}