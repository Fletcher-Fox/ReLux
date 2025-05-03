using UnityEngine;
using TMPro;

public class BattleUnitHUD : MonoBehaviour
{
    [SerializeField] private BattleHUDSO _unitHUD;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moveText;

    void OnEnable()
    {
        _unitHUD = Resources.Load<BattleHUDSO>("SOInstance/Core/BattleHUD");
        _unitHUD.onDataChange.AddListener(UpdateUI);
    }

    void OnDisable()
    {
        _unitHUD.onDataChange.RemoveListener(UpdateUI);
    }

    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.gameObject.SetActive(_unitHUD.visible);
        healthText.gameObject.SetActive(_unitHUD.visible);
        moveText.gameObject.SetActive(_unitHUD.visible);

        if (!_unitHUD.visible) return;

        nameText.text = _unitHUD.characterName;
        healthText.text = $"HP: {_unitHUD.health}";
        moveText.text = $"Move: {_unitHUD.movement}";
    }

}
