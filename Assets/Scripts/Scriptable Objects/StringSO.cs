using UnityEngine;

[CreateAssetMenu(fileName = "NewString", menuName = "Scriptable Objects/Variables/String")]
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
