using UnityEngine;

[CreateAssetMenu(fileName = "NewGameToken", menuName = "Scriptable Objects/Token")]
public class GameTokenSO : ScriptableObject
{
    public delegate void OnRegisterToken(GameObject gameObject);
    public event OnRegisterToken RegisterToken;

    public void TriggerRegister(GameObject token)
    {
        RegisterToken?.Invoke(token);
    }

}
