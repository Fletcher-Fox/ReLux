using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewGameToken", menuName = "Scriptable Objects/Token")]
public class GameTokenSO : ScriptableObject
{
    // public UnityEvent<Vector3> RegisterToken;
    private Dictionary<int, Vector3> _tokenBag = new Dictionary<int, Vector3>();
    private int _lastTokenID = 0;

    public int RegisterToken(Vector3 tokenPosition)
    {
        _lastTokenID++;
        _tokenBag.Add(_lastTokenID, tokenPosition);
        return _lastTokenID;
    }

    public int GetTokenCount() 
    {
        return _tokenBag.Count;
    }

    public void ClearTokenBag()
    {
        _tokenBag = new Dictionary<int, Vector3>();
        _lastTokenID = 0;
    }

}
