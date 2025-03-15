using UnityEngine;

namespace BuildingSystem
{
    public abstract class FsmState
    {
        protected readonly Fsm _fsm;
        protected readonly BuildingPlacer _buildingPlacer;
        protected readonly BuildingPreview _buildingPreview;
        protected readonly GuiController _guiController;
        protected readonly InputController _inputController;
        protected readonly Grid _grid;

        protected readonly BuildingDataBase _buildingsDataBase;
        protected readonly GridData _gridData;

        public FsmState(Fsm fsm, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, BuildingDataBase buildingDataBase, GridData gridData)
        {
            _fsm = fsm;
            _buildingPlacer = buildingPlacer;
            _buildingPreview = buildingPreview;
            _guiController = guiController;
            _inputController = inputController;
            _grid = grid;

            _buildingsDataBase = buildingDataBase;
            _gridData = gridData;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}