using UnityEngine;

public class BillboardOld : MonoBehaviour
{
    [SerializeField] private Vector3 FacingDirection;
    
    [SerializeField] private CameraSO _cameraSO;

    public Vector3 targetDirection = new Vector3(1, 0, 1); // Default direction

    void Start() {

        // Normalize the direction to avoid scaling issues
        Vector3 normalizedDirection = targetDirection.normalized;

        // Ensure the target direction is valid
        if (normalizedDirection.sqrMagnitude > 0.001f)
        {
            // Align the plane's front side to face the direction
            transform.rotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
        }
    }

    void Update() {
        // if (transform.position != FacingDirection) {
        //     transform.position = FacingDirection;
        // }
    }

    private void OnEnable() {
        _cameraSO.BillboardEvent += OnBillboardEvent;
    }

    private void OnDisable() {
        _cameraSO.BillboardEvent -= OnBillboardEvent;
    }

    private void OnBillboardEvent(Vector3 directionToFace) {
        // facing: -5, 3, -5


        FacingDirection = directionToFace;
        

    }
}
