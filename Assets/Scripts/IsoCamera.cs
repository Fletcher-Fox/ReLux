// using UnityEngine;
// using System;
// using UnityEngine.InputSystem;
// using Unity.Cinemachine;

// public class IsoCamera : MonoBehaviour
// {
//     private CinemachineCamera _virtualCamera;
//     private Transform target;

//     public float zoomSpeed = 0.002f;
//     public float minZoom = 10f;
//     public float maxZoom = 25f;
//     public float rotationDuration = 0.1f; // Duration for the rotation animation

//     private CinemachineFollow _transposer;
//     private Vector3 _initialOffset;
//     private Vector3 _targetOffset;
//     private bool _IS_ROTATING;
//     private float _rotationStartTime;

//     public float cameraMoveSpeed = 20f; 
//     // private bool _shiftPressed = false;
//     private float _ZOOM;

//     private PlayerInputActions _inputActions;
    
//     private void Awake()
//     {

//     }

//     private void OnEnable()
//     {
//         _inputActions.Enable();
//         _inputActions.Camera.RotateLeft.performed += RotateCameraLeft;
//         _inputActions.Camera.RotateRight.performed += RotateCameraRight;
//     }

//     private void OnDisable()
//     {
//         _inputActions.Disable();
//         _inputActions.Camera.RotateLeft.performed -= RotateCameraLeft;
//         _inputActions.Camera.RotateRight.performed -= RotateCameraRight;
//     }

//     void Start()
//     {
//         // Get the CinemachineVirtualCamera component attached to this GameObject
//         _virtualCamera = GetComponent<CinemachineCamera>();

//         // Ensure the virtual camera is not null
//         if (_virtualCamera != null)
//         {
//             // Set the follow and look at targets to the same object this script is attached to
//             target = _virtualCamera.Follow;

//             // Get the transposer component
//             _transposer = _virtualCamera.GetCinemachineComponent<CinemachineFollow>();
//             _initialOffset = new Vector3(-10, 10, -10); // Set initial offset to maintain the 45-degree angle
//             _transposer.m_FollowOffset = _initialOffset;
//             _targetOffset = _initialOffset;
//         }
//     }

//     void Update()
//     {
//         Vector2 moveInput = _inputActions.Camera.Move.ReadValue<Vector2>();

//         bool isShiftPressed = _inputActions.Camera.Shift.ReadValue<float>() > 0;

//         Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y);

//         if (inputDirection != Vector3.zero)
//         {
//             inputDirection.Normalize();
//         }

//         Vector3 transformedDirection = _virtualCamera.transform.TransformDirection(inputDirection);
//         Vector3 flatenDirection = new Vector3(transformedDirection.x, 0, transformedDirection.z);

//         if (isShiftPressed)
//         {
//             _virtualCamera.Follow.transform.Translate(flatenDirection * cameraMoveSpeed * 2 * Time.deltaTime, UnityEngine.Space.World);
//         }
//         else
//         {
//             _virtualCamera.Follow.transform.Translate(flatenDirection * cameraMoveSpeed * Time.deltaTime, UnityEngine.Space.World);
//         }

//         if (_transposer != null)
//         {
//             float scroll = _inputActions.Camera.Zoom.ReadValue<float>();
//             _ZOOM = Mathf.Clamp(_transposer.m_FollowOffset.y - scroll * zoomSpeed, minZoom, maxZoom);
//             float zoomFactor = _ZOOM / _initialOffset.y;
//             _transposer.m_FollowOffset = new Vector3(_initialOffset.x * zoomFactor, _ZOOM, _initialOffset.z * zoomFactor);

//             if (_IS_ROTATING)
//             {
//                 float t = (Time.time - _rotationStartTime) / rotationDuration;
//                 _transposer.m_FollowOffset = Vector3.Lerp(_transposer.m_FollowOffset, _targetOffset, t);
//                 if (t >= 1f)
//                 {
//                     _IS_ROTATING = false;
//                     _initialOffset = _targetOffset;
//                 }
//             }
//         }
//     }

//     private void RotateCameraLeft(InputAction.CallbackContext context)
//     {
//         // Debug.Log("Rotate Left");

//         _targetOffset = Quaternion.Euler(0, -90f, 0) * _initialOffset;
//         float zoomFactor = _ZOOM / Mathf.Abs(_targetOffset.y);
//         _targetOffset = new Vector3(_targetOffset.x * zoomFactor, _ZOOM, _targetOffset.z * zoomFactor);

//         StartRotation();
//     }

//     private void RotateCameraRight(InputAction.CallbackContext context)
//     {
//         // Debug.Log("Rotate Right");
//         _targetOffset = Quaternion.Euler(0, 90f, 0) * _initialOffset;

//         float zoomFactor = _ZOOM / Mathf.Abs(_targetOffset.y);
//         _targetOffset = new Vector3(_targetOffset.x * zoomFactor, _ZOOM, _targetOffset.z * zoomFactor);

//         StartRotation();
//     }

//     private void StartRotation()
//     {
//         _IS_ROTATING = true;
//         _rotationStartTime = Time.time;
//     }
// }
