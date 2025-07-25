using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewGameToken", menuName = "Scriptable Objects/Token")]
public class GameTokenSO : ScriptableObject
{
    // public UnityEvent<Vector3> RegisterToken;
    private Dictionary<Vector3, int> _tokenBag = new Dictionary<Vector3, int>();
    private int _lastTokenID = 0;

    public int RegisterToken(Vector3 tokenPosition)
    {
        _lastTokenID++;
        _tokenBag.Add(tokenPosition, _lastTokenID);
        return _lastTokenID;
    }

    public int GetToken(Vector3 vectorKey) 
    {
        return _tokenBag.ContainsKey(vectorKey) ? _tokenBag[vectorKey] : 0;
    }

    public List<int> GetTokens(List<Vector3> vectorKeys)
    {
        List<int> tokenIdList = new List<int>();

        foreach (Vector3 vectorKey in vectorKeys)
        {
            int tokenId = GetToken(vectorKey);

            if (tokenId != 0)
            {
                tokenIdList.Add(tokenId);
            }
        }

        return tokenIdList;
    }

    public int GetTokenCount() 
    {
        return _tokenBag.Count;
    }

    public void ClearTokenBag()
    {
        _tokenBag = new Dictionary<Vector3, int>();
        _lastTokenID = 0;
    }

}
