using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : GameTokenSO
{
    public UnityEvent<Vector3, string> tileClick;
    public UnityEvent<Vector3, string, string> tileEnter;
    public UnityEvent<Vector3> tileExit;
    public UnityEvent<List<int>, Material, string> changeMaterial;
    private BoardSO _board;
    private MaterialsSO _tileMaterials;

    private Dictionary<int, TileData> _tileDataBag; // field

    public Dictionary<int, TileData> TileDataBag // property
    {
        get { return _tileDataBag;}
    }

    // OVERLOAD GameTokenSO RegisterToken()
    public int RegisterToken(Vector3 tokenPosition, string terrain)
    {
        _lastTokenID++;
        _tokenBag.Add(tokenPosition, _lastTokenID);
        _tileDataBag.Add(_lastTokenID, new TileData(terrain, 1));
        return _lastTokenID;
    }

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
    public void OnTileEnter(Vector3 tilePosition, string terrain, string type)
    {
        tileEnter?.Invoke(tilePosition, terrain, type);
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
