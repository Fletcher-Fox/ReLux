using Unity.Mathematics;
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
    }

    void Update() {
        
        Vector3 m = new Vector3(moveInput.x, 0, moveInput.y);

        if (math.abs(m.x) > 0 || math.abs(m.z) > 0)
        {
            m = m.normalized * speed * Time.deltaTime;
            m = RotateWithQuaternion(m, 45);
            transform.Translate(m, Space.World);
        }
    }

    Vector3 RotateWithQuaternion(Vector3 vector, int rotation)
    {
        Quaternion q = Quaternion.Euler(0, rotation, 0); // 90 degrees around Y-axis
        return q * vector;
    }

}
