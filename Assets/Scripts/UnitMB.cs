using UnityEngine;

public class UnitMB : MonoBehaviour
{
    private SpaceSO space_event;

    void OnEnable()
    {
        space_event = Resources.Load<SpaceSO>("SOInstance/Core/Spaces");
        if (space_event != null)
            space_event.SpaceClick += warpUnitTo;
    }

    void OnDisable()
    {
        if (space_event != null)
            space_event.SpaceClick -= warpUnitTo;
    }

    void warpUnitTo(GameObject space) {
        transform.position = space.transform.position;
    }


}
