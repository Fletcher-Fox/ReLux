using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : ScriptableObject
{
    public delegate void OnTileClick(GameObject gameObject);
    public event OnTileClick TileClick;

    public delegate void OnTileEnter(GameObject gameObject);
    public event OnTileEnter TileEnter;

    public delegate void OnTileExit(GameObject gameObject);
    public event OnTileExit TileExit;

    public void TirggerOnTileClick(GameObject tile)
    {  
        TileClick?.Invoke(tile);
    }

    public void TirggerOnTileEnter(GameObject tile)
    {
        TileEnter?.Invoke(tile);
    }

    public void TirggerOnTileExit(GameObject tile)
    {
        TileExit?.Invoke(tile);
    }
}
