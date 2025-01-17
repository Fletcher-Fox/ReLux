using UnityEngine;

[CreateAssetMenu(fileName = "NewSpace", menuName = "Scriptable Objects/Space")]
public class SpaceSO : ScriptableObject
{
    public delegate void OnSpaceClick(GameObject gameObject);
    public event OnSpaceClick SpaceClick;

    public delegate void OnSpaceEnter(GameObject gameObject);
    public event OnSpaceEnter SpaceEnter;

    public delegate void OnSpaceExit(GameObject gameObject);
    public event OnSpaceExit SpaceExit;

    public void TirggerOnSpaceClick(GameObject space)
    {  
        SpaceClick?.Invoke(space);
    }

    public void TirggerOnSpaceEnter(GameObject space)
    {
        SpaceEnter?.Invoke(space);
    }

    public void TirggerOnSpaceExit(GameObject space)
    {
        SpaceExit?.Invoke(space);
    }
}
