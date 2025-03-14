namespace BuldingSystem
{
    public abstract class FsmState
    {
        protected readonly Fsm _fsm;
        protected readonly BuildingPlacer _buildingPlacer;
        protected readonly BuildingPreview _buildingPreview;
        protected readonly GuiController _guiController;

        public FsmState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController)
        {
            _fsm = fsm;
            _buildingPlacer = buildingPlacer;
            _buildingPreview = buildingPreview;
            _guiController = guiController;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}