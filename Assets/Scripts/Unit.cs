using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private TileSO tile_event;
    private UnitSO unit_event;

    [SerializeField] private string unitName = "EVA-00";
    [SerializeField] private int movementRange = 1;

    public string getName()
    {
        return unitName;
    }

    public int getMovementRange()
    {
        return movementRange;
    }

    void OnEnable()
    {
        tile_event = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        unit_event = Resources.Load<UnitSO>("SOInstance/Core/Unit");

        if (tile_event != null)
            tile_event.TileClick += checkIfMyTile;
    }

    void OnDisable()
    {
        if (tile_event != null)
            tile_event.TileClick -= checkIfMyTile;
    }


    void checkIfMyTile(GameObject tile) {
        if (transform.position == tile.transform.position) {
            unit_event.EventUnitClicked(gameObject);
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
