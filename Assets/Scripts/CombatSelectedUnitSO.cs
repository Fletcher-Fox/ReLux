using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HUD/Character HUD Data")]
public class CombatSelectedUnitSO : ScriptableObject
{
    [Header("Character HUD Info")]
    public string characterName;
    public int health;
    public int movement;
    public bool visible;

    [Header("Events")]
    public UnityEvent onHUDDataChanged;

    public void Set(string name, int hp, int move)
    {
        characterName = name;
        health = hp;
        movement = move;
        visible = true;
        onHUDDataChanged?.Invoke();
    }

    public void Clear()
    {
        characterName = string.Empty;
        health = 0;
        movement = 0;
        visible = false;
        onHUDDataChanged?.Invoke();
    }
}
