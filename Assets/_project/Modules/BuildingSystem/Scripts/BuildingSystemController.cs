using UnityEngine;

namespace BuldingSystem
{
    public class BuildingSystemController : MonoBehaviour
    {
        [Tooltip("Data")]
        [SerializeField] private BuildingBase _buildingBase;

        [Tooltip("Services")]
        [SerializeField] private BuildingPreview _buildingPreview;
        [SerializeField] private GuiController _guiController;

        private Fsm _fsm;

        public void Init()
        {
            InitializeStates();
            InitializeServices();
        }

        private void InitializeStates()
        {
            _fsm = new Fsm();
            _fsm.AddState(new IdleState(_fsm));
            _fsm.AddState(new ConstructionState(_fsm));
            _fsm.AddState(new DelectionState(_fsm));

            Debug.Log($"State machine for {name} initialized");
        }

        private void InitializeServices()
        {
            _buildingPreview.Init();
            _guiController.Init();
        }
    }
}