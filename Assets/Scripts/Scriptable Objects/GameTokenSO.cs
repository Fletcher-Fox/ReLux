using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewGameToken", menuName = "Scriptable Objects/Token")]
public class GameTokenSO : ScriptableObject
{
    // public UnityEvent<Vector3> RegisterToken;
    public Dictionary<int, Vector3> _tokenBag = new Dictionary<int, Vector3>();
    private int _tokenID = 0;

    public void RegisterToken(Vector3 tokenPosition)
    {
        _tokenID++;
        _tokenBag.Add(_tokenID, tokenPosition);
    }

    public void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        foreach (KeyValuePair<TKey, TValue> pair in dictionary)
        {
            Debug.Log($"Key: {pair.Key}, Value: {pair.Value}");
        }
    }

}
