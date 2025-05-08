using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // TODO: Refactor logic for prefab of unit this is attached to. The Transfrom Position is at <0, 0.6, 0> Was at <0, 0, 0> and not working before.
    public PlayerCharacterSO characterData;
    private UnitSO _unit;
    [SerializeField] private int _tokenID;

    void Start()
    {
        _tokenID = _unit.RegisterToken(transform.position); // Pass game obj to the board 
    }
    
    void OnEnable()
    {
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        _unit.checkUnit.AddListener(checkUnitTile);
        _unit.checkUnitHover.AddListener(checkUnitTileHoverEnter);

    }

    void OnDisable()
    {
        _unit.checkUnit.RemoveListener(checkUnitTile);
        _unit.checkUnitHover.RemoveListener(checkUnitTileHoverEnter);
    }

    public string GetName()
    {
        return characterData.GetName();
    }

    public int GetHealth()
    {
        return characterData.GetHealth();
    }

    public int GetMovement()
    {
        return characterData.GetMovement();
    }
    void checkUnitTile(int unitID) 
    {
        if (_tokenID == unitID) 
            _unit.EventUnitClicked(transform.position, characterData.GetName(), characterData.GetHealth(), characterData.GetMovement());
    }

    void checkUnitTileHoverEnter(int unitID)
    {
        if (_tokenID == unitID)
            _unit.EventUnitHoverEnter(transform.position, characterData.GetName(), characterData.GetHealth(), characterData.GetMovement());
    }
    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
