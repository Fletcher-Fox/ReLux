using UnityEngine;
using UnityEngine.Events;

public class CharacterSO : ScriptableObject
{  
    private string _name;
    private int _health;
    private int _movement;
    
    public string GetName()
    {
        return _name;
    }
    public void SetName(string n)
    {
        _name = n;
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
