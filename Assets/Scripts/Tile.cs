using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{   

    [SerializeField] private TileSO tile_event;
    [SerializeField] private bool _isSpawnPoint;
    [SerializeField] private MaterialsSO tile_materials;

    void Start() {
        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component
        if (renderer != null && _isSpawnPoint)
        {
            renderer.material = tile_materials.spawn_material; // Set the material
        }
    }

    void OnMouseDown()
    {   
        Debug.Log("Object clicked: " + gameObject.name);
        tile_event.TirggerOnTileClick(gameObject);
    }

    void OnMouseEnter()
    {
        tile_event.TirggerOnTileEnter(gameObject);
    }

    void OnMouseExit()
    {   
        tile_event.TirggerOnTileExit(gameObject);
    }
}
