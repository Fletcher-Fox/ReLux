using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : ScriptableObject
{
    public delegate void UnitClicked(GameObject gameObject);
    public event UnitClicked UnitClickedEvent;

    public void EventUnitClicked(GameObject unit)
    {
        Unit unitScript = unit.GetComponent<Unit>();
        UnitClickedEvent?.Invoke(unit);
        Debug.Log("Unit: Was Clicked : " + unitScript.getName());
    }
}
