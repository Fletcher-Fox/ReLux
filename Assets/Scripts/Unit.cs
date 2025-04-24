using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitSO _unit;

    [SerializeField] private string unitName = "EVA-00";
    [SerializeField] private int unitHealth = 100;
    [SerializeField] private int movementRange = 1;

    void Start() 
    {
        _unit.TriggerRegister(transform.position); // Pass game obj to the board 
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
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        // _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");

        _unit.checkUnit.AddListener(checkUnitTile);
    }

    void OnDisable()
    {
        _unit.checkUnit.RemoveListener(checkUnitTile);
    }


    void checkUnitTile(Vector3 tilePosition) 
    {
        if (transform.position == tilePosition) 
        {
            _unit.EventUnitClicked(transform.position);
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
