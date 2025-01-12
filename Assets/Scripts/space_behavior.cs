using Unity.VisualScripting;
using UnityEngine;

public class space_behavior : MonoBehaviour
{   

    [SerializeField] private space_SO space_event;

    void OnMouseDown()
    {   
        if (gameObject != null)
        {
            space_event.TirggerOnSpaceClick(gameObject);
        }
    }

    // [SerializeField] private materials_SO materials;

    // private bool clicked = false;

    // void OnMouseDown() 
    // {   

    //     Debug.Log("Plane was clicked");
    //     Debug.Log(GetComponent<MeshRenderer>().sharedMaterial.name);

    //     if (!clicked)
    //     {
    //         GetComponent<MeshRenderer>().material = materials.hover_material;
    //         clicked = true;
    //     }
    //     else 
    //     {
    //         GetComponent<MeshRenderer>().material = materials.deafault_material;
    //         clicked = false;
    //     }
    // }

    // void OnMouseEnter()
    // {
    //     if (!clicked)
    //     {
    //         GetComponent<MeshRenderer>().material = materials.select_material;
    //     }
    // }

    // void OnMouseExit()
    // {   
    //     if (!clicked)
    //     {
    //         GetComponent<MeshRenderer>().material = materials.deafault_material;
    //     }
    // }
}
