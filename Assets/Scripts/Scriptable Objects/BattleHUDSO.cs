using System;
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

    public string tilePosition;
    public string tileType;
    public bool tileInfoVisible = false;

    [Header("Events")]
    public UnityEvent onDataChange;
    public UnityEvent onTileChange;
    [SerializeField] private BoardSO _boardData;

    private void OnEnable()
    {
        _boardData = Resources.Load<BoardSO>("SOInstance/Core/Board");
        _boardData.tileHover.AddListener(TileHUD);
        _boardData.unitSelected.AddListener(CheckSelection);
        _boardData.unitHover.AddListener(HoverSet);
        _boardData.clearHUD.AddListener(Clear);
        CheckSelection(Vector3.zero, "", 0, 0);
    }

    private void OnDisable()
    {
        _boardData.unitSelected.RemoveListener(CheckSelection);
        _boardData.unitHover.RemoveListener(HoverSet);
    }

    private void TileHUD(Vector3 position, String type)
    {
        tileInfoVisible = true;
        tilePosition = position + "";
        tileType = type;
        onTileChange?.Invoke();
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

    private void HoverSet(Vector3 unitPosition, string name, int health, int movement)
    {    
        Debug.Log("HOVER SET Battle HUD SO:" + unitPosition);    
        if (unitPosition == Vector3.zero) {
            return;
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
