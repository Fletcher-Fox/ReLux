using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;


public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private GameObject cameraTarget;

    public float speed = 5f; // Movement speed
    public Vector2 moveInput;
    public bool shift;

    [SerializeField] private int currentDirection = 0;
    [SerializeField] private List<Vector3> directions = new List<Vector3>
    {
        new Vector3(-5, 3, -5),
        new Vector3(-5, 3, 5),
        new Vector3(5, 3, 5),
        new Vector3(5, 3, -5)
    };
    

    [Header("Input")]
    [SerializeField] private InputReader _input;


    private void OnEnable() {
        _input.MoveEvent += OnMove;
        _input.ShiftEvent += OnShift;
        _input.QEvent += OnQEvent;
        _input.EEvent += OnEEvent;
    }

    private void OnDisable() {
        _input.MoveEvent -= OnMove;
        _input.ShiftEvent -= OnShift;
        _input.QEvent -= OnQEvent;
        _input.EEvent -= OnEEvent;
    }

    private void OnMove(Vector2 movement) {
        moveInput = movement;
    }

    private void OnShift(bool shiftPressed) {
        shift = shiftPressed;
    }

    private void OnQEvent(bool keyPressed) {
        currentDirection += 1;
        
        if (currentDirection > 3) {
            currentDirection = 0;
        }

        SetFollowOffset(directions[currentDirection]);
    }

    private void OnEEvent(bool keyPressed) {
        currentDirection -= 1;
        
        if (currentDirection < 0) {
            currentDirection = 3;
        }

        SetFollowOffset(directions[currentDirection]);
    }


    void Update()
    {
        // Step 1: Get WASD input
        float horizontalInput = moveInput.x; //Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = moveInput.y; //Input.GetAxis("Vertical");     // W/S or Up/Down

        // Step 2: Get the camera's forward and right vectors
        Vector3 cameraForward = cinemachineCamera.transform.forward;
        Vector3 cameraRight = cinemachineCamera.transform.right;

        // Step 3: Flatten the vectors (ignore y component for horizontal movement)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Step 4: Calculate the movement direction
        Vector3 movementDirection = (cameraForward * verticalInput) + (cameraRight * horizontalInput);

        // Step 5: Check for movement
        if (movementDirection.magnitude > 0)
        {
            // Avoid diagonl speed boost
            if (movementDirection.magnitude > 1) {
                movementDirection.Normalize();
            } 

            if (shift) {
                if (speed < 8) {
                    speed += .25f;
                }
            } else {
                speed = 5;
            }

            // Step 6: Move your character (example)
            cameraTarget.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
            // Debug: Visualize the movement direction
            Debug.DrawLine(transform.position, transform.position + movementDirection, Color.green);
        }
    }

    Vector3 RotateWithQuaternion(Vector3 vector, int rotation)
    {
        Quaternion q = Quaternion.Euler(0, rotation, 0); // 90 degrees around Y-axis
        return q * vector;
    }


    public void ChangeFollowTarget(Transform newTarget)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = newTarget;
        }
    }

    public void SetFollowOffset(Vector3 newOffset)
    {
        if (cinemachineCamera != null)
        {
            CinemachineFollow _transposer = cinemachineCamera.GetComponent<CinemachineFollow>();

            if (_transposer != null) {
                _transposer.FollowOffset = newOffset;
            }
        }
    }
}
