using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : GameTokenSO
{
    public UnityEvent<Vector3> tileClick;
    public UnityEvent<Vector3> tileEnter;
    public UnityEvent<Vector3> tileExit;

    public UnityEvent<List<Vector3>, Material> changeMaterial;
    private BoardSO _board;

    public void OnEnable()
    {
        _board = Resources.Load<BoardSO>("SOInstance/Core/Board");

        _board.changeTileMaterial.AddListener(changeTileMaterials);
    }
    public void OnDisable()
    {
        _board.changeTileMaterial.RemoveListener(changeTileMaterials);
    }

    public void OnTileClick(Vector3 tilePosition)
    {  
        tileClick.Invoke(tilePosition);
    }
    public void OnTileEnter(Vector3 tilePosition)
    {
        tileEnter?.Invoke(tilePosition);
    }
    public void OnTileExit(Vector3 tilePosition)
    {
        tileExit?.Invoke(tilePosition);
    }

    public void changeTileMaterials(List<Vector3> tiles, Material material)
    {   
        changeMaterial.Invoke(tiles, material);
    }
}
