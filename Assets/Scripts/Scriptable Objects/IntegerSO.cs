using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInteger", menuName = "Scriptable Objects/Variables/Integer")]
public class IntegerSO : ScriptableObject
{
    public int Value;
    public delegate void OnValueChange(int value);
    public event OnValueChange ValueChanged;

    public void setValue(int value)
    {
        Value = value;
        ValueChanged?.Invoke(Value);
    }

    public void applyChanges(int changes)
    {
        Value += changes;
        ValueChanged?.Invoke(Value);
    }
}
