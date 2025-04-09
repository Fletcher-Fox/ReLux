using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private IntegerSO integerSO;
    [SerializeField] private int delta;
    void OnMouseDown()
    {   
        integerSO.ApplyChanges(delta);
    }
}
