using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private TileSO _tileEvent;
    private UnitSO _unitEvent;

    [SerializeField] private string unitName = "EVA-00";
    [SerializeField] private int unitHealth = 100;
    [SerializeField] private int movementRange = 1;

    void Start() {
        _unitEvent.TriggerRegister(transform.position); // Pass game obj to the board 
    }

    public string getName()
    {
        return unitName;
    }

    public int getHealth()
    {
        return unitHealth;
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
            _tileEvent.tileClick.AddListener(checkIfMyTile);
    }

    void OnDisable()
    {
        if (_tileEvent != null)
            _tileEvent.tileClick.RemoveListener(checkIfMyTile);
    }


    void checkIfMyTile(Vector3 tilePosition) {
        if (transform.position == tilePosition)
            _unitEvent.EventUnitClicked(transform.position);
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
