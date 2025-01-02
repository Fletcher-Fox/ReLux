using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Vector3 FacingDirection;
    
    [SerializeField] private CameraSO _cameraSO;

    private void OnEnable() {
        _cameraSO.BillboardEvent += OnBillboardEvent;
    }

    private void OnDisable() {
        _cameraSO.BillboardEvent -= OnBillboardEvent;
    }

    private void OnBillboardEvent(Vector3 directionToFace) {
        FacingDirection = directionToFace;
    }

    // void Update()
    // {
    //     // Step 1: Get WASD input
    //     float horizontalInput = moveInput.x; //Input.GetAxis("Horizontal"); // A/D or Left/Right
    //     float verticalInput = moveInput.y; //Input.GetAxis("Vertical");     // W/S or Up/Down

    //     // Step 2: Get the camera's forward and right vectors
    //     Vector3 cameraForward = cinemachineCamera.transform.forward;
    //     Vector3 cameraRight = cinemachineCamera.transform.right;

    //     // Step 3: Flatten the vectors (ignore y component for horizontal movement)
    //     cameraForward.y = 0;
    //     cameraRight.y = 0;
    //     cameraForward.Normalize();
    //     cameraRight.Normalize();

    //     // Step 4: Calculate the movement direction
    //     Vector3 movementDirection = (cameraForward * verticalInput) + (cameraRight * horizontalInput);

    //     // Step 5: Normalize the movement direction
    //     if (movementDirection.magnitude > 1)
    //     {
    //         movementDirection.Normalize();
    //     }

    //     if (shift) {
    //         if (speed < 8) {
    //             speed += .25f;
    //         }
    //     } else {
    //         speed = 5;
    //     }

    //     // Step 6: Move your character (example)
    //     cameraTarget.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);

    //     // Debug: Visualize the movement direction
    //     Debug.DrawLine(transform.position, transform.position + movementDirection, Color.green);
    // }

    // Vector3 RotateWithQuaternion(Vector3 vector, int rotation)
    // {
    //     Quaternion q = Quaternion.Euler(0, rotation, 0); // 90 degrees around Y-axis
    //     return q * vector;
    // }


    // public void ChangeFollowTarget(Transform newTarget)
    // {
    //     if (cinemachineCamera != null)
    //     {
    //         cinemachineCamera.Follow = newTarget;
    //     }
    // }

    // public void SetFollowOffset(Vector3 newOffset)
    // {
    //     if (cinemachineCamera != null)
    //     {
    //         CinemachineFollow _transposer = cinemachineCamera.GetComponent<CinemachineFollow>();

    //         if (_transposer != null) {
    //             _transposer.FollowOffset = newOffset;
    //         }
    //     }
    // }

}