namespace BuildingSystem
{
    public class DelectionState : FsmState
    {
        public DelectionState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController) : base(fsm, buildingPlacer, buildingPreview, guiController)
        {
        }

        public override void Enter()
        {
            _guiController.UpdateStateText("Режим сноса");
        }
    }

}