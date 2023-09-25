using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private GameObject enemyCanvas;
    private GameObject green;
    private RectTransform healthBar;

    public GameObject XPprefab;


    private void Start()
    {
        currentHealth = maxHealth;
        enemyCanvas = gameObject.transform.Find("Canvas").gameObject;
        green = enemyCanvas.transform.Find("Green").gameObject;
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

    private void Die()
    {
        // Handle enemy death here (e.g., play death animation, drop items, etc.)
        Quaternion direction = new Quaternion(0,0,0,0);
        GameObject xp = Instantiate(XPprefab, transform.position, direction);
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
