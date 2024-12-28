using UnityEngine;

public class space_behavior : MonoBehaviour
{   

    [SerializeField] private materials_SO materials;

    private bool clicked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() 
    {   

        Debug.Log("Plane was clicked");
        Debug.Log(GetComponent<MeshRenderer>().sharedMaterial.name);

        if (!clicked)
        {
            GetComponent<MeshRenderer>().material = materials.hover_material;
            clicked = true;
        }
        else 
        {
            GetComponent<MeshRenderer>().material = materials.deafault_material;
            clicked = false;
        }
    }

    void OnMouseEnter()
    {
        if (!clicked)
        {
            GetComponent<MeshRenderer>().material = materials.select_material;
        }
    }

    void OnMouseExit()
    {   
        if (!clicked)
        {
            GetComponent<MeshRenderer>().material = materials.deafault_material;
        }
    }
}
