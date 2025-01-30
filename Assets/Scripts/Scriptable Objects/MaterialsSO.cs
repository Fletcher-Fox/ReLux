using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterials", menuName = "Scriptable Objects/Materials")]
public class MaterialsSO : ScriptableObject
{
    public Material deafault_material;
    public Material hover_material;
    public Material select_material;
    public Material spawn_material;
}
