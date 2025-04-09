using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoard", menuName = "Scriptable Objects/Board")]
public class BoardSO : ScriptableObject
{
    [SerializeField] private TileSO tile_event;
    // [SerializeField] private UnitSO unit_event;
    [SerializeField] private MaterialsSO tile_materials;

    private GameObject selectedTile;
    // private GameObject selectedUnit;


    void OnEnable()
    {
        tile_event.TileClick += onClick;
        tile_event.TileEnter += onEnter;
        tile_event.TileExit += onExit;

        // unit_event = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        // unit_event.UnitClickedEvent += onUnitClicked;
    }

    void OnDisable()
    {
        tile_event.TileClick -= onClick;
        tile_event.TileEnter -= onEnter;
        tile_event.TileExit -= onExit;

        // unit_event.UnitClickedEvent -= onUnitClicked;
    }


    // void onUnitClicked(GameObject unit, String name) 
    // {
    //     if (selectedTile != unit) {
    //         selectedTile = unit;
    //         Debug.Log("Board: Selected Unit: " + name);
    //     }
    // }

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
}
