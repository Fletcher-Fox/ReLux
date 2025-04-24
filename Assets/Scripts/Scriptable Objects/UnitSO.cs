using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : GameTokenSO
{
    private TileSO _tileData;
    public UnityEvent<Vector3> UnitClicked;

    public void EventUnitClicked(Vector3 unitPosition)
    {
        UnitClicked?.Invoke(unitPosition);
    }
}
