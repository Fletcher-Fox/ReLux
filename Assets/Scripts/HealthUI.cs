using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private IntegerSO health;  // Example starting health
    
    void OnEnable()
    {
        health.OnValueChange += UpdateHealthText;
    }

    void OnDisable()
    {
        health.OnValueChange -= UpdateHealthText;
    }

    void Start()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health;
    }
}
