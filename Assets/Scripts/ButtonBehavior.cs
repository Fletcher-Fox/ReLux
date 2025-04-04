using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private IntegerSO integerSO;
    [SerializeField] private int delta;
    void OnMouseDown()
    {   
        integerSO.ApplyChanges(delta);
    }
}
