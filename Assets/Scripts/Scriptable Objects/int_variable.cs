using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "int_variable", menuName = "Scriptable Objects/variables/ int_variable")]
public class int_variable : ScriptableObject
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
