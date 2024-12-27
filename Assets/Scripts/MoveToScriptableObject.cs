using UnityEngine;

public class MoveToScriptableObject : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public Vector2 moveInput;
    [Header("Input")]
    [SerializeField] private InputReader _input;

    private void OnEnable() {
        _input.MoveEvent += OnMove;
    }

    private void OnDisable() {
        _input.MoveEvent -= OnMove;
    }

    

    private void OnMove(Vector2 movement) {

        moveInput = movement;
        Vector3 m = new Vector3(movement.x, 0, movement.y);

        // Normalize to prevent faster diagonal movement and scale by speed
        if (m.magnitude > 1)
        {
            m = m.normalized;
        }
        m *= speed * Time.deltaTime;

        transform.Translate(m, Space.World);
    }

}
