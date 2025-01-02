using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Input/Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _asset;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<bool> ShiftEvent;
    public event UnityAction<bool> QEvent;
    public event UnityAction<bool> EEvent;

    private InputAction _moveAction;
    private InputAction _shiftAction;

    private InputAction _qRotateAction;
    private InputAction _eRotateAction;

    private void OnEnable() {
        _moveAction = _asset.FindAction("Move");
        _shiftAction = _asset.FindAction("Shift");
        _qRotateAction = _asset.FindAction("QRotate");
        _eRotateAction = _asset.FindAction("ERotate");

        _moveAction.started += OnMove;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _shiftAction.started += OnShift;
        _shiftAction.performed += OnShift;
        _shiftAction.canceled += OnShift;

        _qRotateAction.started += OnQRotate;
        _eRotateAction.started += OnERotate;

        _moveAction.Enable();
        _shiftAction.Enable();
        _qRotateAction.Enable();
        _eRotateAction.Enable();
    }

    private void OnDisable() {
        _moveAction.started -= OnMove;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _shiftAction.started -= OnShift;
        _shiftAction.performed -= OnShift;
        _shiftAction.canceled -= OnShift;

        _qRotateAction.started -= OnQRotate;
        _eRotateAction.started -= OnERotate;

        _moveAction.Disable();
        _shiftAction.Disable();
        _qRotateAction.Disable();
        _eRotateAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnShift(InputAction.CallbackContext context) {
        ShiftEvent?.Invoke(context.ReadValue<float>() > 0);
    }

    private void OnQRotate(InputAction.CallbackContext context) {
        QEvent?.Invoke(context.ReadValue<float>() > 0);
    }

    private void OnERotate(InputAction.CallbackContext context) {
        EEvent?.Invoke(context.ReadValue<float>() > 0);
    }
}
