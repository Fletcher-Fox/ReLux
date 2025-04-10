using UnityEngine;

public class CheckCast : MonoBehaviour
{

    void CheckHit(Transform start, Transform end)
    {
        Vector3 direction = end.position - start.position;
        float distance = direction.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(start.position, direction.normalized, out hit, distance))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }
    }


}
