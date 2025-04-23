using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : GameTokenSO
{
    public UnityEvent<Vector3> tileClick;
    public UnityEvent<Vector3> tileEnter;
    public UnityEvent<Vector3> tileExit;

    public void OnTileClick(Vector3 tilePosition)
    {  
        tileClick?.Invoke(tilePosition);
    }
    public void OnTileEnter(Vector3 tilePosition)
    {
        tileEnter?.Invoke(tilePosition);
    }
    public void OnTileExit(Vector3 tilePosition)
    {
        tileExit?.Invoke(tilePosition);
    }

}
