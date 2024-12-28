using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Input/Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _asset;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<Boolean> ShiftEvent;
    private InputAction _moveAction;
    private InputAction _shiftAction;

    private void OnEnable() {
        _moveAction = _asset.FindAction("Move");
        _shiftAction = _asset.FindAction("Shift");

        _moveAction.started += OnMove;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _shiftAction.started += OnShift;
        _shiftAction.performed += OnShift;
        _shiftAction.canceled += OnShift;

        _moveAction.Enable();
        _shiftAction.Enable();
    }

    private void OnDisable() {
        _moveAction.started -= OnMove;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _shiftAction.started -= OnShift;
        _shiftAction.performed -= OnShift;
        _shiftAction.canceled -= OnShift;

        _moveAction.Disable();
        _shiftAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnShift(InputAction.CallbackContext context) {
        ShiftEvent?.Invoke(context.ReadValue<float>() > 0);
    }
}
