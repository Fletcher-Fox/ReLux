using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : GameTokenSO
{
    private TileSO _tile;
    private BoardSO _board;
    public UnityEvent<Vector3> unitClicked;
    public UnityEvent<Vector3> checkUnit;

    public void OnEnable()
    {
        _tile = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");

        _tile.tileClick.AddListener(CheckUnitTile);
    }
    public void OnDisable()
    {
        _tile.tileClick.RemoveListener(CheckUnitTile);
    }

    private void CheckUnitTile(Vector3 tilePosition)
    {
        checkUnit.Invoke(tilePosition);
    }

    public void EventUnitClicked(Vector3 unitPosition)
    {
        unitClicked?.Invoke(unitPosition);
    }
}
