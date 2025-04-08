using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInteger", menuName = "Scriptable Objects/Variables/Integer")]
public class IntegerSO : ScriptableObject
{
    public int Value = 100;
    public delegate void OnValueChange(int value);
    public event OnValueChange ValueChanged;

    public void SetValue(int value)
    {
        Value = value;
        ValueChanged?.Invoke(Value);
    }

    public void ApplyChanges(int changes)
    {
        Value += changes;
        ValueChanged?.Invoke(Value);
    }
}
