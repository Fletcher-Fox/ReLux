using UnityEngine;

[CreateAssetMenu(fileName = "space_SO", menuName = "Scriptable Objects/space_SO")]
public class space_SO : ScriptableObject
{
    public delegate void OnSpaceClick(GameObject gameObject);
    public event OnSpaceClick SpaceClick;

    public void TirggerOnSpaceClick(GameObject space)
    {  
            SpaceClick?.Invoke(space);
    }
}
