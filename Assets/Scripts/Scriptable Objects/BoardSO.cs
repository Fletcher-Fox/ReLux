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


    void OnEnable()
    {
        tile_event.TileClick += onClick;
        tile_event.TileEnter += onEnter;
        tile_event.TileExit += onExit;

        unit_event = Resources.Load<UnitSO>("SOInstance/Core/Unit"); // TODO: I believe this is being saved in a bad way...
        unit_event.UnitClickedEvent += onUnitClicked;
    }

    void OnDisable()
    {
        tile_event.TileClick -= onClick;
        tile_event.TileEnter -= onEnter;
        tile_event.TileExit -= onExit;

        unit_event.UnitClickedEvent -= onUnitClicked;
    }


    void onUnitClicked(GameObject unit)
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

    void update_material(GameObject tile, Material material)
    {
        tile.GetComponent<MeshRenderer>().material = material;
    }

    void onClick(GameObject tile)
    {    
        if (tile == selectedTile)
        {
            selectedTile = null;
            update_material(tile, tile_materials.deafault_material);
        }
        else 
        {   
            if (selectedTile != null)
            {
                update_material(selectedTile, tile_materials.deafault_material);
            }

            selectedTile = tile;
            update_material(tile, tile_materials.select_material);
        }
    }

    void onEnter(GameObject tile)
    {   
        update_material(tile, tile_materials.hover_material);
    }

    void onExit(GameObject tile)
    {   
        if (tile == selectedTile)
        {
            update_material(tile, tile_materials.select_material); 
        }
        else 
        {
            update_material(tile, tile_materials.deafault_material);
        }
    }



    void CheckHit(Vector3 startPosition, Vector3 endPosition)
    {
        // Calculate the direction and distance using Vector3
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        Debug.Log("-------------------------------------------------");
        Debug.Log("Start: " + startPosition + ", End: " + endPosition);

        Debug.Log("Distance: " + distance);

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
            Debug.Log("Hit nothing");
        
            // Draw a ray to show the path even if it doesn't hit anything
            Debug.DrawRay(startPosition, direction, Color.green, 2f); // Ray will stay visible for 2 seconds
        }

        Debug.Log("-------------------------------------------------");
    }
}
