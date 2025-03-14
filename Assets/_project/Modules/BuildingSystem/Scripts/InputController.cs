using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class InputController : MonoBehaviour
    {
        private BuldingSystemControls _inputActions;

        public void Init()
        {
            _inputActions = new BuldingSystemControls();
            _inputActions.Enable();
            ActivateEvents();
        }

        public void ActivateEvents()
        {
            _inputActions.BuildingMap.LeftMousePress.performed += OnLeftMouseClicked;
            _inputActions.BuildingMap.EscapeButtonPress.performed += OnEscapeClicked;
        }

        public void OnLeftMouseClicked(InputAction.CallbackContext context)
        {
            Debug.Log("Left mouse clicked");
        }

        public void OnEscapeClicked(InputAction.CallbackContext context)
        {
            Debug.Log("Escape button clicked");
        }

        public Vector2 ReadMousePosition()
        {
            Vector2 mousePosition = _inputActions.BuildingMap.CursorPosition.ReadValue<Vector2>();
            return mousePosition;
        }

        private void OnDisable()
        {
            _inputActions.BuildingMap.LeftMousePress.performed -= OnLeftMouseClicked;
            _inputActions.BuildingMap.EscapeButtonPress.performed -= OnEscapeClicked;
        }
    }
}