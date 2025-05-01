using UnityEngine;
using UnityEngine.Events;

public class CharacterSO : ScriptableObject
{  
    [SerializeField] private string _name;
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _baseMovement;

    private int _health;
    private int _movement;

    void OnEnable()
    {
        _health = _baseHealth;
        _movement = _baseMovement;
    }

    public string GetName()
    {
        return _name;
    }
 
    public int GetHealth() 
    {
        return _health;
    }
    public void SetHealth(int hp)
    {
        _health = hp;
    } 
    public int GetMovement()
    {
        return _movement;
    }
    public void SetMovement(int mv)
    {
        _movement = mv;
    }
}
