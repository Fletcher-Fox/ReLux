using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Scriptable Objects/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private StringSO CharacterName;
    [SerializeField] private IntegerSO Health;
    [SerializeField] private IntegerSO CurrentHealth;
    [SerializeField] private IntegerSO Movement;
    [SerializeField] private IntegerSO CurrentMovement;    

}
