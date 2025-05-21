using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : GameTokenSO
{
    public UnityEvent<Vector3, string> tileClick;
    public UnityEvent<Vector3, string> tileEnter;
    public UnityEvent<Vector3> tileExit;
    public UnityEvent<List<int>, Material, string> changeMaterial;
    private BoardSO _board;
    private MaterialsSO _tileMaterials;

    public void OnEnable()
    {
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");
        _tileMaterials = Resources.Load<MaterialsSO>("SOInstance/Core/Materials"); // TODO: remove and replace with ref to a const file
        _board.tileChange.AddListener(TileChange);
    }
    public void OnDisable()
    {
        _board.tileChange.RemoveListener(TileChange);
    }

    public void OnTileClick(Vector3 tilePosition, string tileType)
    {
        tileClick.Invoke(tilePosition, tileType);
    }
    public void OnTileEnter(Vector3 tilePosition, string terrain)
    {
        tileEnter?.Invoke(tilePosition, terrain);
    }
    public void OnTileExit(Vector3 tilePosition)
    {
        tileExit?.Invoke(tilePosition);
    }

    public void TileChange(List<Vector3> tiles, string type)
    {
        List<int> tileIds = GetTokens(tiles);
        changeMaterial?.Invoke(tileIds, FetchMaterial(type), type);
    }

    public Material FetchMaterial(string type)
    {
        switch (type)
        {
            case "select":
                return _tileMaterials.select_material;
            case "movement":
                return _tileMaterials.movement_material;
            case "spawn":
                return _tileMaterials.spawn_material;
            case "default":
            default:
                return _tileMaterials.default_material;
        }
    }
}
