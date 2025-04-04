using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private IntegerSO health;  // Example starting health
    
    void OnEnable()
    {
        health.ValueChanged += UpdateHealthText;
    }

    void OnDisable()
    {
        health.ValueChanged -= UpdateHealthText;
    }

    void Start()
    {
        UpdateHealthText(health.Value);
    }

    private void UpdateHealthText(int h)
    {
        healthText.text = "Health: " + h;
    }
}
