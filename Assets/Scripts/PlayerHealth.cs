using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100.0f;
    public float currentHealth;
    private GameObject playerCanvas;
    private GameObject green;
    private RectTransform healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        playerCanvas = Camera.main.gameObject.transform.Find("Canvas").gameObject.transform.Find("Canvas_Health").gameObject;
        green = playerCanvas.transform.Find("Green").gameObject;
        healthBar = green.GetComponent<RectTransform>();
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x,3*currentHealth/maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x,3*currentHealth/maxHealth);

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int Health)
    {
        currentHealth += Health;
        maxHealth += Health;
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x,3*currentHealth/maxHealth);
    }

    private void Die()
    {
        // Handle enemy death here (e.g., play death animation, drop items, etc.)

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
