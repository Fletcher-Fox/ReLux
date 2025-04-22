using UnityEngine;
using TMPro;

public class CombatUnitHUD : MonoBehaviour
{
    [SerializeField] private CombatSelectedUnitSO _unitData;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moveText;

    private void OnEnable()
    {
        _unitData = Resources.Load<CombatSelectedUnitSO>("SOInstance/Core/Combat Unit HUD");
        _unitData.onDataChange.AddListener(UpdateUI);
        UpdateUI();
    }

    private void OnDisable()
    {
        _unitData.onDataChange.RemoveListener(UpdateUI);
    }

    private void UpdateUI()
    {
        gameObject.SetActive(_unitData.visible);

        if (!_unitData.visible) return;

        nameText.text = _unitData.characterName;
        healthText.text = $"HP: {_unitData.health}";
        moveText.text = $"Move: {_unitData.movement}";
    }

}
