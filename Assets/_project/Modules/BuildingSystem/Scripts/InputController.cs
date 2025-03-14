using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

namespace BuildingSystem
{
    public class InputController : MonoBehaviour
    {
        private BuldingSystemControls _inputActions;

        public event Action OnLeftMouseButtonClicked;
        public event Action OnEscapeButtonClicked;

        public void Init()
        {
            _inputActions = new BuldingSystemControls();
            _inputActions.Enable();

            EnableInputEvents();
        }

        public void EnableInputEvents()
        {
            _inputActions.BuildingMap.LeftMousePress.performed += LeftMouseButtonClicked;
            _inputActions.BuildingMap.EscapeButtonPress.performed += EscapeButtonClicked;
        }

        public bool IsCursorOverUI() => EventSystem.current.IsPointerOverGameObject();

        public void LeftMouseButtonClicked(InputAction.CallbackContext context) => OnLeftMouseButtonClicked?.Invoke();
        public void EscapeButtonClicked(InputAction.CallbackContext context) => OnEscapeButtonClicked?.Invoke();
       
        public Vector2 ReadMousePosition()
        {
            Vector2 mousePosition = _inputActions.BuildingMap.CursorPosition.ReadValue<Vector2>();
            return mousePosition;
        }

        private void OnDisable()
        {
            _inputActions.BuildingMap.LeftMousePress.performed -= LeftMouseButtonClicked;
            _inputActions.BuildingMap.EscapeButtonPress.performed -= EscapeButtonClicked;
        }
    }
}