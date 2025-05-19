using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterials", menuName = "Scriptable Objects/Materials")]
public class MaterialsSO : ScriptableObject
{
    public Material default_material;
    public Material hover_material;
    public Material select_material;
    public Material spawn_material;
    public Material movement_material;
}
