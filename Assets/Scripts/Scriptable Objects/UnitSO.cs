using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : ScriptableObject
{
    public delegate void UnitClicked(GameObject gameObject, String name);
    public event UnitClicked UnitClickedEvent;

    public void EventUnitClicked(GameObject unit, String name)
    {
        UnitClickedEvent?.Invoke(unit, name);
        Debug.Log("Unit: Was Clicked : " + name);
    }
}
