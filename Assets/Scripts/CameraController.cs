using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;


public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private GameObject targetPointer;
    

    public float speed = 5f; // Movement speed
    public Vector2 moveInput;
    public bool shift;
    private float _scrollOffset = 2;

    [SerializeField] private int currentDirection = 0;
    private List<Vector3> directions = new List<Vector3>
    {
        new Vector3(-3, 2, -3),
        new Vector3(-3, 2, 3),
        new Vector3(3, 2, 3),
        new Vector3(3, 2, -3)
    };
    

    [Header("Input")]
    [SerializeField] private InputReaderSO _input;

    [Header("Zoom Settings")]
    private float _zoomSpeed = 3f;
    private float _minZoom = 1.5f;
    private float _maxZoom = 5f;

    private CinemachineFollow _transposer;

    private void Start()
    {
        // Get the CinemachineFollow component
        if (cinemachineCamera != null)
        {
            _transposer = cinemachineCamera.GetComponent<CinemachineFollow>();
        }
    }

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
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        // Step 1: Get WASD input
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

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
            if (movementDirection.magnitude > 1)
            {
                movementDirection.Normalize();
            }

            if (shift)
            {
                if (speed < 8)
                {
                    speed += .25f;
                }
            }
            else
            {
                speed = 5;
            }

            // Step 6: Move your character
            cameraTarget.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
            Debug.DrawLine(transform.position, transform.position + movementDirection, Color.green);

            targetPointer.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        }
    }


    private void HandleZoom()
    {
        if (_transposer == null) return;

        // Get scroll input (positive for zooming out, negative for zooming in)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            Vector3 newOffset = _transposer.FollowOffset;
            newOffset.y -= scrollInput * _zoomSpeed;
            _scrollOffset = Mathf.Clamp(newOffset.y, _minZoom, _maxZoom);
            newOffset.y = _scrollOffset;
            _transposer.FollowOffset = newOffset;
        }
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
            // CinemachineFollow _transposer = cinemachineCamera.GetComponent<CinemachineFollow>();

            if (_transposer != null) {
                // _transposer.FollowOffset = newOffset;
                TransitionFollowOffset(newOffset, 0.25f);
            }
        }
    }


    public void TransitionFollowOffset(Vector3 targetOffset, float transitionDuration)
    {
        if (_transposer != null)
        {
            StartCoroutine(SlerpFollowOffset(targetOffset, transitionDuration));
        }
    }



    private IEnumerator SlerpFollowOffset(Vector3 newOffset, float duration)
    {

        Vector3 initialOffset = _transposer.FollowOffset;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 offset = Vector3.Slerp(initialOffset, newOffset, elapsedTime / duration);
            _transposer.FollowOffset = new Vector3(offset.x, _scrollOffset, offset.z);
            // _transposer.FollowOffset = Vector3.Lerp(initialOffset, newOxffset, elapsedTime / duration);
            yield return null;
        }

        _transposer.FollowOffset = new Vector3(newOffset.x, _scrollOffset, newOffset.z); // Ensure exact final value
    }

}
