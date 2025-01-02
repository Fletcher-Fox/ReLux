using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(fileName = "CameraSO", menuName = "Scriptable Objects/CameraSO")]
public class CameraSO : ScriptableObject
{
    [SerializeField] private InputActionAsset _asset;

    private InputAction _qRotateAction;
    private InputAction _eRotateAction;

    public event UnityAction<Vector3> BillboardEvent;

    [SerializeField] private int currentDirection = 0;

    [SerializeField] private List<Vector3> directions = new List<Vector3>
    {
        new Vector3(-5, 3, -5),
        new Vector3(-5, 3, 5),
        new Vector3(5, 3, 5),
        new Vector3(5, 3, -5)
    };

    private Vector3 Billboard() {
        return -directions[currentDirection];
    }

    private void OnEnable() {
        _qRotateAction = _asset.FindAction("QRotate");
        _eRotateAction = _asset.FindAction("ERotate");

        _qRotateAction.started += OnRotate;
        _eRotateAction.started += OnRotate;

        _qRotateAction.Enable();
        _eRotateAction.Enable();
    }

    private void OnDisable() {
        _qRotateAction.started -= OnRotate;
        _eRotateAction.started -= OnRotate;

        _qRotateAction.Disable();
        _eRotateAction.Disable();
    }

    private void OnRotate(InputAction.CallbackContext context) {
        // Get the control that triggered the action
        if (context.control is KeyControl keyControl)
        {
            if (keyControl.keyCode == Key.Q) {
                currentDirection += 1;
                if (currentDirection > 3) {
                    currentDirection = 0;
                }
            }

            if (keyControl.keyCode == Key.E) {
                currentDirection -= 1;
                if (currentDirection < 0) {
                    currentDirection = 3;
                }
            }
        }

        BillboardEvent?.Invoke(Billboard());
    }
}
