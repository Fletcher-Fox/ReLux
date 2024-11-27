using UnityEngine;

public class space_behavior : MonoBehaviour
{   

    [SerializeField] private Material default_material;
    [SerializeField] private Material lightBlue_material;
    [SerializeField] private Material yellow_material;

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
            GetComponent<MeshRenderer>().material = lightBlue_material;
            clicked = true;
        }
        else 
        {
            GetComponent<MeshRenderer>().material = default_material;
            clicked = false;
        }
    }

    void OnMouseEnter()
    {
        if (!clicked)
        {
            GetComponent<MeshRenderer>().material = yellow_material;
        }
    }

    void OnMouseExit()
    {   
        if (!clicked)
        {
            GetComponent<MeshRenderer>().material = default_material;
        }
    }
}
