using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInteger", menuName = "Scriptable Objects/Variables/Integer")]
public class IntegerSO : ScriptableObject
{
    public int Value;

    void setValue(int value)
    {
        Value = value;
    }

    void applyChanges(int changes)
    {
        Value += changes;
    }
}
