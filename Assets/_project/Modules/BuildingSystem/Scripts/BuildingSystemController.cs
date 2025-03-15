using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSystemController : MonoBehaviour
    {
        [Tooltip("Data")]
        [SerializeField] private BuildingDataBase _buildingsDataBase;
        [SerializeField] private LayerMask _buildingLayerMask;
        private GridData _gridData;

        [Tooltip("Services")]
        [SerializeField] private BuildingPlacer _buildingPlacer;
        [SerializeField] private BuildingPreview _buildingPreview;
        [SerializeField] private GuiController _guiController;
        [SerializeField] private InputController _inputController;
        [SerializeField] private Grid _grid;
        [SerializeField] private PositionCalculator _positionCalculator;

        [Tooltip("Components")]
        Camera _camera;
        private Fsm _fsm;

        public void Init()
        {
            _camera = Camera.main;

            InitializeServices();
            InitializeStates();
        }

        private void InitializeServices()
        {
            _gridData = new GridData();

            _buildingPlacer.Init();
            _buildingPreview.Init();
            _guiController.Init();
            _inputController.Init();
            _positionCalculator.Init(_camera, _buildingLayerMask);
        }

        private void InitializeStates()
        {
            _fsm = new Fsm();
            _fsm.AddState(new IdleState(_fsm, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase,  _gridData));
            _fsm.AddState(new ConstructionState(_fsm, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase, _gridData));
            _fsm.AddState(new DelectionState(_fsm, _buildingPlacer, _buildingPreview, _guiController, _inputController, _grid, _positionCalculator, _buildingsDataBase, _gridData));

            _fsm.SetState<IdleState>();

            Debug.Log($"State machine for {name} initialized");
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}