using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HUD/Character HUD Data")]
public class BattleHUDSO : ScriptableObject
{

    [Header("Character HUD Info")]
    public string characterName;
    public int health;
    public int movement;
    public bool visible = true;

    [Header("Events")]
    public UnityEvent onDataChange;
    [SerializeField] private BoardSO _boardData;

    private void OnEnable()
    {
        _boardData = Resources.Load<BoardSO>("SOInstance/Core/Board");
        _boardData.unitSelected.AddListener(CheckSelection);
        CheckSelection(Vector3.zero, null);
    }

    private void OnDisable()
    {
        _boardData.unitSelected.RemoveListener(CheckSelection);
    }

    private void CheckSelection(Vector3 unitPosition, PlayerCharacterSO character = null)
    {
        Debug.Log("BattleHUD! : " + character.GetName());
        if (unitPosition == Vector3.zero) {
            Clear();
        } else {
            Set(character.GetName(), character.GetHealth(), character.GetMovement());
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
