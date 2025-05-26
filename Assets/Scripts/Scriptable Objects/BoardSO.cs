using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewBoard", menuName = "Scriptable Objects/Board")]
public class BoardSO : ScriptableObject
{
    private TileSO _tile;
    private UnitSO _unit;
    private HashSet<Vector3> _movementTiles = new HashSet<Vector3>(); //empty set
    [SerializeField] private Vector3 _selectedUnitPosition;
    
    public UnityEvent<Vector3, string, int, int> unitSelected;
    public UnityEvent<Vector3, string, int, int> unitHover;
    public UnityEvent<Vector3, string> tileHover;
    public UnityEvent<List<Vector3>, string> tileChange;
    public UnityEvent clearHUD;
    public UnityEvent<bool, Vector3> reticleEvent;

    void OnEnable()
    {
        _tile = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");

        if (_tile == null || _unit == null)
        {
            Debug.LogError("BoardSO failed to load resources.");
            return;
        }

        _tile.tileClick.AddListener(TileOnClick);
        _tile.tileEnter.AddListener(TileOnEnter);
        _tile.tileExit.AddListener(TileOnExit);

        _unit.unitClicked.AddListener(OnUnitClicked);
        _unit.unitHoverEnter.AddListener(UnitOnEnter);
    }

    void OnDisable()
    {
        _tile.tileClick.RemoveListener(TileOnClick);
        _tile.tileEnter.RemoveListener(TileOnEnter);
        _tile.tileExit.RemoveListener(TileOnExit);

        _unit.unitClicked.RemoveListener(OnUnitClicked);
        _unit.unitHoverEnter.RemoveListener(UnitOnEnter);
    }

    public void ClearBoardTokens() 
    {
        _tile.ClearTokenBag();
        _unit.ClearTokenBag();
    }

    void OnUnitClicked(Vector3 unitPosition, string name, int hp, int movement) // TODO: replace name, hp & movement with Object Data
    {
        if (_selectedUnitPosition == unitPosition)
        {
            _selectedUnitPosition = new Vector3(0, 0, 0);
            HideMovement();
        }
        else
        {
            _selectedUnitPosition = unitPosition;
            DisplayMovement(unitPosition, movement);
        }
    }

    private void DisplayMovement(Vector3 unitPosition, int unitMovement)
    {
        // Need to remove previous movement if it exists
        if (_movementTiles?.Count > 0) {
            tileChange?.Invoke(new List<Vector3>(_movementTiles), "default");
            _movementTiles.Clear();
        }

        List<Vector3> cardinalDirections = new List<Vector3> {
            Vector3.left, Vector3.right, Vector3.forward, Vector3.back
        }; 

        HashSet<Vector3> MovementSet = new HashSet<Vector3> {unitPosition};
        HashSet<Vector3> tempSet;
        int start = 0;

        while (start < unitMovement) {
            start += 1;
            tempSet = new HashSet<Vector3>();

            foreach (Vector3 pos in MovementSet) {   
                foreach (Vector3 direction in cardinalDirections) {
                    Vector3 map_pos = pos + direction;
                    Vector3 realPosition = ConfirmObject(CheckHit(pos, map_pos));

                    if (realPosition != Vector3.zero)
                        tempSet.Add(map_pos);
                }
            }

            MovementSet.UnionWith(tempSet);
        }

        HashSet<Vector3> temp = new HashSet<Vector3>(MovementSet);
        foreach (Vector3 v in temp) {
            if (_unit.IsUnit(v))
                MovementSet.Remove(v);
        }
        _movementTiles = MovementSet;
        tileChange?.Invoke(new List<Vector3>(MovementSet), "movement");
    }

    private void HideMovement()
    {
        tileChange?.Invoke(new List<Vector3>(_movementTiles), "default"); // Change all current movement tiles to deafault 
        _movementTiles.Clear();
    }
    Vector3 ConfirmObject(GameObject thing)
    {
        if (thing != null) {
            switch (thing.tag)
            {
                case "Unit":
                    return thing.transform.position;
                case "Tile":
                    return thing.transform.GetChild(0).position;
            }
        }
        return Vector3.zero;
    }

    void TileOnClick(Vector3 tilePosition, string tileType) 
    {
        Debug.Log("Enter Tile:" + tilePosition + " (" + tileType + ")");

        if (tilePosition != _selectedUnitPosition)
        {
            if (tileType == "movement")
            {
                _unit.MoveUnitTo(_selectedUnitPosition, tilePosition);
            }
            // TODO: refactor passing values like this into OnUnitClicked() needs to be an obj or something...
            OnUnitClicked(_selectedUnitPosition, "", 0, 0); // Change the HUD as the Selected Tile and Selected Unit no longer match
            HideMovement(); // Change all current movement tiles to deafault 
            _movementTiles.Clear(); // Make the movement set empty now
        }
    }

    void TileOnEnter(Vector3 tilePosition, string terrain)
    {
        tileHover?.Invoke(tilePosition, terrain);
        reticleEvent?.Invoke(true, tilePosition);
    }

    void UnitOnEnter(Vector3 unitPosition, string name, int hp, int movement)
    {
        unitHover?.Invoke(unitPosition, name, hp, movement);
    }

    void TileOnExit(Vector3 tilePosition) // TODO: Do we need tilePosition??? Look into later...
    {   
        clearHUD?.Invoke();
        reticleEvent?.Invoke(false, Vector3.zero);
    }


    GameObject CheckHit(Vector3 startPosition, Vector3 endPosition)
    {
        // Calculate the direction and distance using Vector3
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        // Perform the raycast with the calculated direction and distance
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction.normalized, out hit, distance))
        {

            // Debug.DrawRay(startPosition, direction, Color.red, distance); // Ray will stay visible for 2 seconds
            return hit.collider.gameObject;
            
            // Debug.Log("Hit object: " + hit.collider.gameObject.name);
            // Draw a ray with the specified color (e.g., red)  
            // // You can also do other things with the hit object, e.g., change color
            // GameObject hitObject = hit.collider.gameObject;
            // hitObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            // Draw a ray to show the path even if it doesn't hit anything
            Debug.DrawRay(startPosition, direction, Color.green, distance); // Ray will stay visible for 2 seconds
            return null;
        }

    }
}
