using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewBoard", menuName = "Scriptable Objects/Board")]
public class BoardSO : ScriptableObject
{
    [SerializeField] private TileSO tile_event;
    [SerializeField] private UnitSO unit_event;
    [SerializeField] private MaterialsSO tile_materials;

    private GameObject selectedTile;
    private GameObject selectedUnit;
    [SerializeField] private List<GameObject> boardTiles;
    void OnEnable()
    {
        boardTiles = new List<GameObject>();
        tile_event.RegisterTile += RegisterTile;
        tile_event.TileClick += OnClick;
        tile_event.TileEnter += OnEnter;
        tile_event.TileExit += OnExit;

        unit_event = Resources.Load<UnitSO>("SOInstance/Core/Unit"); // TODO: I believe this is being saved in a bad way...
        unit_event.UnitClickedEvent += OnUnitClicked;
    }

    void OnDisable()
    {
        tile_event.RegisterTile -= RegisterTile;
        tile_event.TileClick -= OnClick;
        tile_event.TileEnter -= OnEnter;
        tile_event.TileExit -= OnExit;

        unit_event.UnitClickedEvent -= OnUnitClicked;
    }

    void RegisterTile(GameObject tile_space)
    {
        boardTiles.Add(tile_space);
    }

    public void ClearTiles() 
    {
        boardTiles = new List<GameObject>();
    }
    

    void OnUnitClicked(GameObject unit)
    {
        Unit u = unit.GetComponent<Unit>();

        if (selectedUnit != unit) {
            selectedUnit = unit;
            Debug.Log("Board: Selected Unit: " + u.getName());
        }

        Transform unitTransform = unit.transform;

        if (unitTransform != null) {
            Vector3 start = new Vector3(unitTransform.position.x, unitTransform.position.y, unitTransform.position.z);
            Vector3 end = new Vector3(unitTransform.position.x + 1, unitTransform.position.y, unitTransform.position.z);

            CheckHit(start, end);
        }
    }

    void UpdateMaterial(GameObject tile, Material material)
    {
        tile.GetComponent<MeshRenderer>().material = material;
    }

    void OnClick(GameObject tile)
    {    
        if (tile == selectedTile)
        {
            selectedTile = null;
            UpdateMaterial(tile, tile_materials.deafault_material);
        }
        else 
        {   
            if (selectedTile != null)
            {
                UpdateMaterial(selectedTile, tile_materials.deafault_material);
            }

            selectedTile = tile;
            UpdateMaterial(tile, tile_materials.select_material);
        }
    }

    void OnEnter(GameObject tile)
    {   
        UpdateMaterial(tile, tile_materials.hover_material);
    }

    void OnExit(GameObject tile)
    {   
        if (tile == selectedTile)
        {
            UpdateMaterial(tile, tile_materials.select_material); 
        }
        else 
        {
            UpdateMaterial(tile, tile_materials.deafault_material);
        }
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
