using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public PlayerCharacterSO characterData;
    private UnitSO _unit;
    [SerializeField] private int _tokenID;

    void Start()
    {
        _tokenID = _unit.RegisterToken(transform.position); // Pass game obj to the board 
        Debug.Log("Units Token Bag Size: " + _unit.GetTokenCount());
    }
    
    void OnEnable()
    {
        _unit = Resources.Load<UnitSO>("SOInstance/Core/Unit");
        _unit.checkUnit.AddListener(checkUnitTile);
    }

    void OnDisable()
    {
        _unit.checkUnit.RemoveListener(checkUnitTile);
    }

    public string GetName()
    {
        return characterData.GetName();
    }

    public int GetHealth()
    {
        return characterData.GetHealth();
    }

    public int GetMovement()
    {
        return characterData.GetMovement();
    }
    void checkUnitTile(Vector3 tilePosition) 
    {
        if (transform.position == tilePosition) 
        {
            _unit.EventUnitClicked(transform.position, characterData.GetName(), characterData.GetHealth(), characterData.GetMovement());
        }
    }


    // void warpUnitTo(GameObject space) {
    //     transform.position = space.transform.position;
    // }

}
