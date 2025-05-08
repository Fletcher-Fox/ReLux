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
        _tile.tileEnter.RemoveListener(IsUnitOnTile);
    }

    private void IsUnitOnTile(Vector3 tilePosition)
    {
        int unitID = GetToken(tilePosition);
        if (unitID == 0) return;
        checkUnit?.Invoke(unitID);
    }

    private void IsUnitOnHoverTile(Vector3 tilePosition)
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

}
