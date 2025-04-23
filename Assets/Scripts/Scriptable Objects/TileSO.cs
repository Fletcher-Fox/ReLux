using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTile", menuName = "Scriptable Objects/Tile")]
public class TileSO : GameTokenSO
{
    public UnityEvent<Vector3> tileClick;
    public UnityEvent<Vector3> tileEnter;
    public UnityEvent<Vector3> tileExit;

    public UnityEvent<Vector3, Material> changeMaterial;
    private BoardSO _boardEvent;

    public void OnEnable()
    {
        _boardEvent = Resources.Load<BoardSO>("SOInstance/Core/Board");

        _boardEvent.changeTileMaterial.AddListener(changeTileMaterials);
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

    public void changeTileMaterials(Vector3 tilePosition, Material material)
    {   
        changeMaterial.Invoke(tilePosition, material);
    }
}
