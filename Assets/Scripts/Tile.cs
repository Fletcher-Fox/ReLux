using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private int _tokenID;
    [SerializeField] private bool _isSpawnPoint;

    [SerializeField] private String _type;
    // [SerializeField] private MaterialsSO _tileMaterials;

    void Start() {
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _tokenID = _tileEvent.RegisterToken(transform.position); // Pass game obj to the board 

        // _tileMaterials = Resources.Load<MaterialsSO>("SOInstance/Core/Materials");

        // Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component
        // if (renderer != null && _isSpawnPoint)
        // {
        //     renderer.material = _tileMaterials.spawn_material; // Set the material
        // }

        _tileEvent.changeMaterial.AddListener(materialChange);
    }

    void OnMouseDown()
    {
        _tileEvent.OnTileClick(transform.position);
    }

    void OnMouseEnter()
    {
        _tileEvent.OnTileEnter(transform.position, _type);
    }

    void OnMouseExit()
    {   
        _tileEvent.OnTileExit(transform.position, _type);
    }

    void materialChange(List<int> tiles, Material material)
    {   
        if (tiles.Contains(_tokenID))
        {
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
