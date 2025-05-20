using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : GameTokenSO
{
    private TileSO _tile;
    private BoardSO _board;
    public UnityEvent<Vector3, string, int, int> unitClicked;
    public UnityEvent<Vector3, string, int, int> unitHoverEnter;
    public UnityEvent<int> checkUnit;
    public UnityEvent<int> checkUnitHover;

    public UnityEvent<int, Vector3> moveUnit;

    public void OnEnable()
    {
        _tile = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");

        _tile.tileClick.AddListener(IsUnitOnTile);
        _tile.tileEnter.AddListener(IsUnitOnHoverTile);
    }
    public void OnDisable()
    {
        _tile.tileClick.RemoveListener(IsUnitOnTile);
        _tile.tileEnter.RemoveListener(IsUnitOnHoverTile);
    }

    private void IsUnitOnTile(Vector3 tilePosition, string type) // TODO: we just ignore the type?
    {
        int unitID = GetToken(tilePosition);
        if (unitID == 0) return;
        checkUnit?.Invoke(unitID);
    }

    private void IsUnitOnHoverTile(Vector3 tilePosition, string tileType) //TODO: is this the best way? Just ignore the args not meant for this?
    {
        int unitID = GetToken(tilePosition);
        if (unitID == 0) return;
        checkUnitHover?.Invoke(unitID);
    }

    public void EventUnitClicked(Vector3 unitPosition, string name, int hp, int movement)
    {
        unitClicked?.Invoke(unitPosition, name, hp, movement);
    }

    public void EventUnitHoverEnter(Vector3 unitPosition, string name, int hp, int movement)
    {
        unitHoverEnter?.Invoke(unitPosition, name, hp, movement);
    }

    public bool IsUnit(Vector3 position)
    {
        int unitID = GetToken(position);
        if (unitID == 0)
            return false;
        else
            return true;
    }

    public void MoveUnitTo(Vector3 original, Vector3 position)
    {
        int id = GetToken(original); // Get unit id
        ReregisterToken(original, position); // Change unit to new key in dictionary
        moveUnit?.Invoke(id, position); // Tell unit to move to new position
    }

}
