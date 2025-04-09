using UnityEngine;

public class Unit : MonoBehaviour
{
    private TileSO tile_event;
    private UnitSO unit_event;

    [SerializeField] private string unitName = "EVA-00";

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
        Debug.Log("Tile Pos: " + tile.transform.position);
        if (transform.position == tile.transform.position) {
            Debug.Log("Unit On Same Tile!");
            unit_event.EventUnitClicked(gameObject, unitName);
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
