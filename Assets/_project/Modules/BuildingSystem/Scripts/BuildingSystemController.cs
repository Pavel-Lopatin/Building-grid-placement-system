using BuildingSystem;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSystemController : MonoBehaviour
    {
        [Tooltip("Data")]
        [SerializeField] private BuildingBase _buildingBase;

        [Tooltip("Services")]
        [SerializeField] private BuildingPlacer _buildingPlacer;
        [SerializeField] private BuildingPreview _buildingPreview;
        [SerializeField] private GuiController _guiController;
        [SerializeField] private InputController _inputController;

        private Fsm _fsm;

        public void Init()
        {
            InitializeServices();
            InitializeStates();
        }

        private void InitializeServices()
        {
            _buildingPlacer.Init();
            _buildingPreview.Init();
            _guiController.Init();
            _inputController.Init();
        }

        private void InitializeStates()
        {
            _fsm = new Fsm();
            _fsm.AddState(new IdleState(_fsm, _buildingPlacer, _buildingPreview, _guiController));
            _fsm.AddState(new ConstructionState(_fsm, _buildingPlacer, _buildingPreview, _guiController));
            _fsm.AddState(new DelectionState(_fsm, _buildingPlacer, _buildingPreview, _guiController));

            _fsm.SetState<IdleState>();

            Debug.Log($"State machine for {name} initialized");
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}