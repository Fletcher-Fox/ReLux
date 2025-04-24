using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HUD/Character HUD Data")]
public class CombatSelectedUnitSO : ScriptableObject
{

    [Header("Character HUD Info")]
    public string characterName;
    public int health;
    public int movement;
    public bool visible = true;

    [Header("Events")]
    public UnityEvent onDataChange;
    // [SerializeField] private BoardSO _boardData;

    private void OnEnable()
    {
        // _boardData = Resources.Load<BoardSO>("SOInstance/Core/Board");
        // _boardData.selectedUnit.AddListener(CheckSelection);
        // CheckSelection(null);
    }

    private void OnDisable()
    {
        // _boardData.selectedUnit.RemoveListener(CheckSelection);
    }

    private void CheckSelection(GameObject unit)
    {
        if (unit == null) {
            Clear();
        } else {
            Unit u = unit.GetComponent<Unit>();
            Debug.Log("UNIT HUD:" + u.getName());
            Set(u.getName(), u.getHealth(), u.getMovementRange());
        }
    }

    public void Set(string name, int hp, int move)
    {
        characterName = name;
        health = hp;
        movement = move;
        visible = true;
        onDataChange?.Invoke();
    }

    public void Clear()
    {
        characterName = string.Empty;
        health = 0;
        movement = 0;
        visible = false;
        onDataChange?.Invoke();
    }
}
