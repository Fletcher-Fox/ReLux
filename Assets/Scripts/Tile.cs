using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private bool _isSpawnPoint;
    // [SerializeField] private MaterialsSO _tileMaterials;

    void Start() {
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _tileEvent.TriggerRegister(transform.position); // Pass game obj to the board 
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
        _tileEvent.OnTileEnter(transform.position);
    }

    void OnMouseExit()
    {   
        _tileEvent.OnTileExit(transform.position);
    }

    void materialChange(List<Vector3> tiles, Material material)
    {   
        if (tiles.Contains(transform.position))
        {
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
