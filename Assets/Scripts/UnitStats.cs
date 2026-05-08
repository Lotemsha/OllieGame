using CoreClasses.Models;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour
{
    [Header("General Settings")]
    public Enemy enemyData; 
    public PlayerManager playerData;

    public string unitName;
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackDamage = 10f;

    [Header("UI Reference")]
    public Slider healthSlider;

    void Awake()
    {
        // בתחילת הקרב, החיים מלאים
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // הגנה שהחיים לא ירדו מתחת ל-0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(unitName + " נשאר עם: " + currentHealth + " חיים");

        // עדכון ויזואלי של הסליידר
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(unitName + " Defeated!");
    }
}