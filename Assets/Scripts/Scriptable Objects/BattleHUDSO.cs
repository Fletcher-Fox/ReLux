using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HUD/BattleHUDSO")]
public class BattleHUDSO : ScriptableObject
{

    [Header("Character HUD Info")]
    public string characterName;
    public int health;
    public int movement;
    public bool visible = false;

    [Header("Events")]
    public UnityEvent onDataChange;
    [SerializeField] private BoardSO _boardData;

    private void OnEnable()
    {
        _boardData = Resources.Load<BoardSO>("SOInstance/Core/Board");
        _boardData.unitSelected.AddListener(CheckSelection);
        CheckSelection(Vector3.zero, "", 0, 0);
    }

    private void OnDisable()
    {
        _boardData.unitSelected.RemoveListener(CheckSelection);
    }

    private void CheckSelection(Vector3 unitPosition, string name, int health, int movement)
    {
        Debug.Log("Battle HUD SO:" + unitPosition);
        if (unitPosition == Vector3.zero) {
            Clear();
        } else {
            Set(name, health, movement);
        }
    }

    public void Set(string name, int hp, int move)
    {
        Debug.Log("SET!");
        characterName = name;
        health = hp;
        movement = move;
        visible = true;
        onDataChange?.Invoke();
    }

    public void Clear()
    {
        Debug.Log("CLEAR!");
        characterName = "";
        health = 0;
        movement = 0;
        visible = false;
        onDataChange?.Invoke();
    }
}
