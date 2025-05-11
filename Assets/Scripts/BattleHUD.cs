using UnityEngine;
using TMPro;
using System.Linq;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private BattleHUDSO _unitHUD;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moveText;

    [SerializeField] private TMP_Text tileTypeText;
    [SerializeField] private TMP_Text tilePositionText;

    void OnEnable()
    {
        _unitHUD = Resources.Load<BattleHUDSO>("SOInstance/Core/BattleHUD");
        _unitHUD.onDataChange.AddListener(UpdateUI);
        _unitHUD.onTileChange.AddListener(UpdateUI);
    }

    void OnDisable()
    {
        _unitHUD.onDataChange.RemoveListener(UpdateUI);
        _unitHUD.onTileChange.RemoveListener(UpdateUI);
    }

    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        tileTypeText.gameObject.SetActive(_unitHUD.tileInfoVisible);
        tilePositionText.gameObject.SetActive(_unitHUD.tileInfoVisible);
        
        nameText.gameObject.SetActive(_unitHUD.visible);
        healthText.gameObject.SetActive(_unitHUD.visible);
        moveText.gameObject.SetActive(_unitHUD.visible);

        tileTypeText.text = _unitHUD.tileType;
        tilePositionText.text = _unitHUD.tilePosition;

        nameText.text = _unitHUD.characterName;
        healthText.text = $"HP: {_unitHUD.health}";
        moveText.text = $"Move: {_unitHUD.movement}";
    }

}
