using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewGameToken", menuName = "Scriptable Objects/Token")]
public class GameTokenSO : ScriptableObject
{
    // public UnityEvent<Vector3> RegisterToken;
    private List<Vector3> _tokenBag;

    public void TriggerRegister(Vector3 tokenPosition)
    {
        // RegisterToken?.Invoke(tokenPosition);
        _tokenBag.Add(tokenPosition);
    }

}
