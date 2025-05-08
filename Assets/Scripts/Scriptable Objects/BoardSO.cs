using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "NewBoard", menuName = "Scriptable Objects/Board")]
public class BoardSO : ScriptableObject
{
    // public UnityEvent<GameObject> selectedUnit;

    private TileSO _tile;
    private UnitSO _unit;
    private MaterialsSO _tileMaterials;

    private Vector3 _selectedTile;
    private Vector3 _selectedUnitPosition;
    
    public UnityEvent<Vector3, string, int, int> unitSelected;
    public UnityEvent<Vector3, string, int, int> unitHover;
    public UnityEvent clearHUD;
    public UnityEvent<List<Vector3>, Material> changeTileMaterial;

    void OnEnable()
    {
        _tile = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        _tileMaterials = Resources.Load<MaterialsSO>("SOInstance/Core/Materials"); // TODO: remove and replace with ref to a const file

        _tile.tileClick.AddListener(OnClick);
        _tile.tileEnter.AddListener(OnEnter);
        _tile.tileExit.AddListener(OnExit);

        _unit.unitClicked.AddListener(OnUnitClicked);
        _unit.unitHoverEnter.AddListener(UnitOnEnter);
    }

    void OnDisable()
    {
        _tile.tileClick.RemoveListener(OnClick);
        _tile.tileEnter.RemoveListener(OnEnter);
        _tile.tileExit.RemoveListener(OnExit);

        _unit.unitClicked.RemoveListener(OnUnitClicked);
        _unit.unitHoverEnter.AddListener(UnitOnEnter);
    }

    public void ClearBoardTokens() 
    {
        _tile.ClearTokenBag();
        _unit.ClearTokenBag();
    }
    
    void OnUnitClicked(Vector3 unitPosition, string name, int hp, int movement)
    {   
        // TODO: Need to make menu when a unit is clicked.

        // Debug.Log("BOARD: ON UNIT : position: " + unitPosition + ", name: " + name);

        // if (_selectedUnitPosition == unitPosition) {
        //     _selectedUnitPosition = new Vector3(0,0,0);
        // } else {
        //     _selectedUnitPosition = unitPosition;
        // }
        // unitSelected?.Invoke(_selectedUnitPosition, name, hp, movement);
    }


    void OnClick(Vector3 tilePosition) 
    {
        List<Vector3> tiles = new List<Vector3>{tilePosition};

        if (tilePosition == _selectedTile) {
            _selectedTile = new Vector3(0, 0, 0); // no tiles should be here, reserved for empty position...
            changeTileMaterial.Invoke(tiles, _tileMaterials.hover_material);  // Deselect on same location
        } else {   
            if (_selectedTile != new Vector3(0, 0, 0)) {
                changeTileMaterial?.Invoke(new List<Vector3>{_selectedTile}, _tileMaterials.deafault_material); // Revert original tile back to default material
            }
            
            _selectedTile = tilePosition;
            changeTileMaterial?.Invoke(tiles, _tileMaterials.select_material);
            
            if (_selectedTile != _selectedUnitPosition) {
                OnUnitClicked(_selectedUnitPosition, "", 0, 0); // Change the HUD as the Selected Tile and Selected Unit no longer match
            }
        }
    }

    void OnEnter(Vector3 tilePosition)
    {
        List<Vector3> tiles = new List<Vector3>{tilePosition};

        if (tilePosition != _selectedTile) {
            changeTileMaterial?.Invoke(tiles, _tileMaterials.hover_material);
        }
    }

    void UnitOnEnter(Vector3 unitPosition, string name, int hp, int movement)
    {
        unitHover?.Invoke(unitPosition, name, hp, movement);
    }

    void OnExit(Vector3 tilePosition)
    {   
        List<Vector3> tiles = new List<Vector3>{tilePosition};
        clearHUD?.Invoke();

        if (tilePosition == _selectedTile) {
            changeTileMaterial?.Invoke(tiles, _tileMaterials.select_material); 
        } else {
            changeTileMaterial?.Invoke(tiles, _tileMaterials.deafault_material);
        }
    }

    void CheckHit(Vector3 startPosition, Vector3 endPosition)
    {
        // Calculate the direction and distance using Vector3
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        // Perform the raycast with the calculated direction and distance
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction.normalized, out hit, distance)) {
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
