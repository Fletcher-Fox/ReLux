using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{   

    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private bool _isSpawnPoint;
    [SerializeField] private MaterialsSO _tileMaterials;

    void Start() {
        // _tileEvent.TriggerRegister(this); // Pass game obj to the board 
        Debug.Log("gameObject : " + gameObject);
        Debug.Log("Tile Event: " + _tileEvent);
        _tileEvent.TriggerRegister(gameObject);
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
