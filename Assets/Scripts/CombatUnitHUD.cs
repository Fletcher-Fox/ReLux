using UnityEngine;
using TMPro;

public class BattleUnitHUD : MonoBehaviour
{
    [SerializeField] private BattleHUDSO _unitHUD;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moveText;

    private void OnEnable()
    {
        _unitHUD = Resources.Load<BattleHUDSO>("SOInstance/Core/Combat Unit HUD");
        _unitHUD.onDataChange.AddListener(UpdateUI);
        UpdateUI();
    }

    private void OnDisable()
    {
        _unitHUD.onDataChange.RemoveListener(UpdateUI);
    }

    private void UpdateUI()
    {
        gameObject.SetActive(_unitHUD.visible);

        if (!_unitHUD.visible) return;

        nameText.text = _unitHUD.characterName;
        healthText.text = $"HP: {_unitHUD.health}";
        moveText.text = $"Move: {_unitHUD.movement}";
    }

}
