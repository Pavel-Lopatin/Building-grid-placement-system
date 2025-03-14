namespace BuldingSystem
{
    public class IdleState : FsmState
    {
        public IdleState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController) : base(fsm, buildingPlacer, buildingPreview, guiController)
        {
        }

        public override void Enter()
        {
            _guiController.UpdateStateText("Выберите здание для постройки");
        }
        
    }

}