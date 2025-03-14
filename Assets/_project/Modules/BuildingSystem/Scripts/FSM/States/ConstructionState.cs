namespace BuildingSystem
{
    public class ConstructionState : FsmState
    {
        public ConstructionState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController) : base(fsm, buildingPlacer, buildingPreview, guiController)
        {
        }

        public override void Enter()
        {
            _guiController.UpdateStateText("Режим строиельства");
        }
    }
}