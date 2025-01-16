using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Board_SO", menuName = "Scriptable Objects/Board_SO")]
public class Board_SO : ScriptableObject
{
    [SerializeField] private space_SO space_event;
    [SerializeField] private materials_SO space_materials;

    private GameObject selected;

    void OnEnable()
    {
        space_event.SpaceClick += onClick;
        space_event.SpaceEnter += onEnter;
        space_event.SpaceExit += onExit;
    }

    void OnDisable()
    {
        space_event.SpaceClick -= onClick;
        space_event.SpaceEnter -= onEnter;
    }

    void update_material(GameObject space, Material material)
    {
        space.GetComponent<MeshRenderer>().material = material;
    }

    void onClick(GameObject space)
    {    
        if (space == selected)
        {
            selected = null;
            update_material(space, space_materials.deafault_material);
        }
        else 
        {   
            if (selected != null)
            {
                update_material(selected, space_materials.deafault_material);
            }

            selected = space;
            update_material(space, space_materials.select_material);
        }
    }

    void onEnter(GameObject space)
    {   
        update_material(space, space_materials.hover_material);
    }

    void onExit(GameObject space)
    {   
        if (space == selected)
        {
            update_material(space, space_materials.select_material); 
        }
        else 
        {
            update_material(space, space_materials.deafault_material);
        }
    }
}
