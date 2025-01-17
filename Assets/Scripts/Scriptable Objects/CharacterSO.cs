using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Scriptable Objects/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private StringSO CharacterName;

}
