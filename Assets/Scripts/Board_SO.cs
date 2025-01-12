using UnityEngine;

[CreateAssetMenu(fileName = "Board_SO", menuName = "Scriptable Objects/Board_SO")]
public class Board_SO : ScriptableObject
{
    [SerializeField] private space_SO space_event;
    [SerializeField] private materials_SO space_materials;

    void OnEnable()
    {
        space_event.SpaceClick += onClick;
    }

    void OnDisable()
    {
        space_event.SpaceClick -= onClick;
    }

    void update_material(GameObject space, Material material)
    {
        space.GetComponent<MeshRenderer>().material = material;
    }

    void onClick(GameObject space)
    {    
        if (space.GetComponent<MeshRenderer>().material.color !=  space_materials.select_material.color)
        {
            update_material(space, space_materials.select_material);
        }
        else 
        {
            update_material(space, space_materials.deafault_material);
        }
    }
}
