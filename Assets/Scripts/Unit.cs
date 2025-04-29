using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public PlayerCharacterSO characterData;
    private UnitSO _unit;
    [SerializeField] private int _tokenID;
    [SerializeField] private string _name = "";
    [SerializeField] private int _health = 0;
    [SerializeField] private int _movement = 1;
    [SerializeField] private int _level = 1;

    void Start() 
    {
        _tokenID = _unit.RegisterToken(transform.position); // Pass game obj to the board 
        Debug.Log("Units Token Bag Size: " + _unit.GetTokenCount());
    }

    public string getName()
    {
        return _name;
    }

    public int getHealth()
    {
        return _health;
    }

    public int getMovementRange()
    {
        return _movement;
    }

    private void LoadCharacterData() 
    {
        if (characterData != null) {
            _name = characterData.GetName();
            _health = characterData.GetHealth();
            _movement = characterData.GetMovement();
            Debug.Log("Character :" + _name);
            Debug.Log(" HP:" + _health);
            Debug.Log(" MV:" + _movement);
        } else {
            Debug.Log("!!!ERROR!!! Character Data Could Not Be Found!");
        }
    }

    void OnEnable()
    {
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        LoadCharacterData();
        _unit.checkUnit.AddListener(checkUnitTile);
    }

    void OnDisable()
    {
        _unit.checkUnit.RemoveListener(checkUnitTile);
    }


    void checkUnitTile(Vector3 tilePosition) 
    {
        if (transform.position == tilePosition) 
        {
            _unit.EventUnitClicked(transform.position);
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
