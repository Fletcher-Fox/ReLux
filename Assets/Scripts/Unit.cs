using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private TileSO _tileEvent;
    private UnitSO _unitEvent;

    [SerializeField] private string unitName = "EVA-00";
    [SerializeField] private int movementRange = 1;

    void Start() {
        _unitEvent.TriggerRegister(gameObject); // Pass game obj to the board 
    }

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
        _unitEvent = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");

        if (_tileEvent != null)
            _tileEvent.TileClick += checkIfMyTile;
    }

    void OnDisable()
    {
        if (_tileEvent != null)
            _tileEvent.TileClick -= checkIfMyTile;
    }


    void checkIfMyTile(GameObject tile) {
        if (transform.position == tile.transform.position) {
            _unitEvent.EventUnitClicked(gameObject);
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
