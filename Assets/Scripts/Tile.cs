using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{   

    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private bool _isSpawnPoint;
    [SerializeField] private MaterialsSO _tileMaterials;

    void Start() {
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _tileMaterials = Resources.Load<MaterialsSO>("SOInstance/Core/Materials");

        _tileEvent.TriggerRegister(gameObject); // Pass game obj to the board 

        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component
        if (renderer != null && _isSpawnPoint)
        {
            renderer.material = _tileMaterials.spawn_material; // Set the material
        }
    }

    void OnMouseDown()
    {   
        Debug.Log("Object clicked: " + gameObject.name);
        _tileEvent.TirggerOnTileClick(gameObject);
    }

    void OnMouseEnter()
    {
        _tileEvent.TirggerOnTileEnter(gameObject);
    }

    void OnMouseExit()
    {   
        _tileEvent.TirggerOnTileExit(gameObject);
    }
}
