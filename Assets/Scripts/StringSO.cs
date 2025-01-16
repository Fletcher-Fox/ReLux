using UnityEngine;

[CreateAssetMenu(fileName = "StringSO", menuName = "Scriptable Objects/variables/StringSO")]
public class StringSO : ScriptableObject
{
    public string value;

    void setValue(string str)
    {
        value = str;
    }

    string getValue()
    {
        return value;
    }

}
