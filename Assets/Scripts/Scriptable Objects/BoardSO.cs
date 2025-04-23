using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewBoard", menuName = "Scriptable Objects/Board")]
public class BoardSO : ScriptableObject
{
    // public UnityEvent<GameObject> selectedUnit;

    private TileSO _tileEvent;
    private UnitSO _unitEvent;
    private MaterialsSO _tileMaterials;

    private Vector3 _selectedTile;
    private Vector3 _selectedUnit;

    void OnEnable()
    {
        ClearBoardTokens();
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _unitEvent = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        // _tileMaterials = Resources.Load<MaterialsSO>("SOInstance/Core/Materials");

        _tileEvent.tileClick.AddListener(OnClick);
        _tileEvent.tileEnter.AddListener(OnEnter);
        _tileEvent.tileExit.AddListener(OnExit);

        // _unitEvent.UnitClickedEvent += OnUnitClicked;
    }

    void OnDisable()
    {
        _tileEvent.tileClick.RemoveListener(OnClick);
        _tileEvent.tileEnter.RemoveListener(OnEnter);
        _tileEvent.tileExit.RemoveListener(OnExit);

        // _unitEvent.UnitClickedEvent -= OnUnitClicked;
    }

    // void RegisterTile(GameObject tile)
    // {
    //     _boardTiles.Add(tile);
    // }
    // void RegisterUnit(GameObject unit)
    // {
    //     _boardUnits.Add(unit);
    // }
    public void ClearBoardTokens() 
    {
        // _boardTiles = new List<GameObject>();
        // _boardUnits = new List<GameObject>();
    }
    
    // void OnUnitClicked(GameObject unit)
    // {
    //     Unit u = unit.GetComponent<Unit>();

    //     if (_selectedUnit != unit) {
    //         _selectedUnit = unit;
    //         // selectedUnit?.Invoke(unit);
    //         Debug.Log("Board: Selected Unit: " + u.getName());
    //     }

    //     // *** PATHING TBC... ***
    //     // Transform unitTransform = unit.transform;
    //     // if (unitTransform != null) {
    //     //     Vector3 start = new Vector3(unitTransform.position.x, unitTransform.position.y, unitTransform.position.z);
    //     //     Vector3 end = new Vector3(unitTransform.position.x + 1, unitTransform.position.y, unitTransform.position.z);
    //     //     CheckHit(start, end);
    //     // }
    // }

    // void UpdateMaterial(GameObject tile, Material material)
    // {
    //     tile.GetComponent<MeshRenderer>().material = material;
    // }

    void OnClick(Vector3 tilePosition)
    {
        // DeselectUnit(null);
        if (tilePosition == _selectedTile)
        {
            _selectedTile = new Vector3(0, 0, 0); // no tiles should be here, reserved for empty position...
            // _tileEvent. TODO: Tell TileSO that original tile position (_selectedTile) back to default material...
        //     // selectedUnit?.Invoke(null);
        //     // UpdateMaterial(tile, _tileMaterials.deafault_material);
        }
        else 
        {   
            if (_selectedTile != null)
            {
                // _tileEvent. TODO: Tell TileSO that new slected tile position (_selectedTile) to selected material...
                // UpdateMaterial(_selectedTile, _tileMaterials.deafault_material);
            }

        //     _selectedTile = tile;
        //     // UpdateMaterial(tile, _tileMaterials.select_material);
        }
    }

    // void DeselectUnit(GameObject tile)
    // {
    //     if (_selectedUnit != null && _selectedUnit.transform.position != tile.transform.position) {
    //         Debug.Log("Prev Selected unit (" + _selectedUnit.GetComponent<Unit>().getName() + ") nulled!");
    //         _selectedUnit = null;
    //     }
    // }

    void OnEnter(Vector3 tilePosition)
    {
        // UpdateMaterial(tile, _tileMaterials.hover_material);
    }

    void OnExit(Vector3 tilePosition)
    {   
        // if (tile == _selectedTile)
        // {
        //     UpdateMaterial(tile, _tileMaterials.select_material); 
        // }
        // else 
        // {
        //     UpdateMaterial(tile, _tileMaterials.deafault_material);
        // }
    }



    void CheckHit(Vector3 startPosition, Vector3 endPosition)
    {
        // Calculate the direction and distance using Vector3
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        // Perform the raycast with the calculated direction and distance
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction.normalized, out hit, distance))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            // Draw a ray with the specified color (e.g., red)  
            Debug.DrawRay(startPosition, direction, Color.red, 2f); // Ray will stay visible for 2 seconds

            // You can also do other things with the hit object, e.g., change color
            GameObject hitObject = hit.collider.gameObject;
            hitObject.GetComponent<Renderer>().material.color = Color.red;
        } else {        
            // Draw a ray to show the path even if it doesn't hit anything
            Debug.DrawRay(startPosition, direction, Color.green, 2f); // Ray will stay visible for 2 seconds
        }

    }
}
