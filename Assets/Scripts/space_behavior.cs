using UnityEngine;

public class space_behavior : MonoBehaviour
{   

    [SerializeField] private Material default_material;
    [SerializeField] private Material lightBlue_material;

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

        if (GetComponent<MeshRenderer>().sharedMaterial.name == "Default")
        {
            GetComponent<MeshRenderer>().material = lightBlue_material;
        }
        else 
        {
            GetComponent<MeshRenderer>().material = default_material;
        }
    }
}
