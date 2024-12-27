using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Input/Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _asset;

    public event UnityAction<Vector2> MoveEvent;
    private InputAction _moveAction;

    private void OnEnable() {
        _moveAction = _asset.FindAction("Move");

        _moveAction.started += OnMove;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _moveAction.Enable();
    }

    private void OnDisable() {
        _moveAction.started -= OnMove;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _moveAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

}
