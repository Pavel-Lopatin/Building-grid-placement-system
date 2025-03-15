using UnityEngine;

namespace BuildingSystem
{
    public abstract class FsmState
    {
        protected readonly Fsm _fsm;
        protected readonly BuildingSystemController _controller;
        protected readonly BuildingPlacer _buildingPlacer;
        protected readonly BuildingPreview _buildingPreview;
        protected readonly GuiController _guiController;
        protected readonly InputController _inputController;
        protected readonly Grid _grid;
        protected readonly PositionCalculator _positionCalculator;

        protected readonly BuildingDataBase _buildingsDataBase;
        protected readonly GridData _gridData;

        protected int _lastID;

        public FsmState(Fsm fsm,BuildingSystemController controller, BuildingPlacer buildingPlacer, BuildingPreview buildingPreview, GuiController guiController, InputController inputController, Grid grid, PositionCalculator positionCalculator, BuildingDataBase buildingDataBase, GridData gridData)
        {
            _fsm = fsm;
            _controller = controller;
            _buildingPlacer = buildingPlacer;
            _buildingPreview = buildingPreview;
            _guiController = guiController;
            _inputController = inputController;
            _grid = grid;
            _positionCalculator = positionCalculator;

            _buildingsDataBase = buildingDataBase;
            _gridData = gridData;

        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}