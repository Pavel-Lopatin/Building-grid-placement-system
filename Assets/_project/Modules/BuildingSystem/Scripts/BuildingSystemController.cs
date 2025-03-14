using UnityEngine;

namespace BuldingSystem
{
    public class BuildingSystemController : MonoBehaviour
    {
        [SerializeField] private BuildingBase _buildingBase;
        [SerializeField] private GuiController _guiController;

        private Fsm _fsm;

        public void Init()
        {
            InitializeStates();
            _guiController.Init();
        }

        private void InitializeStates()
        {
            _fsm = new Fsm();
            _fsm.AddState(new IdleState(_fsm));
            _fsm.AddState(new ConstructionState(_fsm));
            _fsm.AddState(new DelectionState(_fsm));

            Debug.Log($"State machine for {name} initialized");
        }
    }
}