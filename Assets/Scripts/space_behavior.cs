using Unity.VisualScripting;
using UnityEngine;

public class space_behavior : MonoBehaviour
{   

    [SerializeField] private space_SO space_event;

    void OnMouseDown()
    {   
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
