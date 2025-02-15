using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image[] hearts;
    private float currentValue;

    private void Update()
    {
        if (currentValue != playerHealth.currentHealth)
        {
            currentValue = playerHealth.currentHealth;
            UpdateHealthbar();
        }
    }

    public void UpdateHealthbar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (currentValue >= i + 1)
            {
                hearts[i].fillAmount = 1f; // Full heart
            }
            else if (currentValue > i)
            {
                hearts[i].fillAmount = currentValue - i; // Partial heart
            }
            else
            {
                hearts[i].fillAmount = 0f; // Empty heart
            }
        }
    }
}
