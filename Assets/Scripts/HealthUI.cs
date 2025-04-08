using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private IntegerSO health;  // Example starting health
    
    void Start()
    {
        UpdateHealthText(health.Value);
    }

    void OnEnable()
    {
        health = Resources.Load<IntegerSO>("SOInstance/Ody/CurrentHealth");
        if (health != null)
            health.ValueChanged += UpdateHealthText;
    }

    void OnDisable()
    {
        if (health != null)
            health.ValueChanged -= UpdateHealthText;
    }

    private void UpdateHealthText(int h)
    {
        healthText.text = "Health: " + h;
    }
}
