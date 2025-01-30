using Unity.VisualScripting;
using UnityEngine;

public class SpaceBehavior : MonoBehaviour
{   

    [SerializeField] private SpaceSO space_event;
    [SerializeField] private bool _isSpawnPoint;
    [SerializeField] private MaterialsSO space_materials;

    void Start() {
        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component
        if (renderer != null && _isSpawnPoint)
        {
            renderer.material = space_materials.spawn_material; // Set the material
        }
    }

    void OnMouseDown()
    {   
        Debug.Log("Object clicked: " + gameObject.name);
        space_event.TirggerOnSpaceClick(gameObject);
    }

    void OnMouseEnter()
    {
        space_event.TirggerOnSpaceEnter(gameObject);
    }

    void OnMouseExit()
    {   
        space_event.TirggerOnSpaceExit(gameObject);
    }
}
