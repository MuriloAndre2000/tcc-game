using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private GameObject enemyCanvas;
    private GameObject green;
    private RectTransform healthBar;

    public GameObject XPprefab;

    public GameObject prefabDamageText;

    public int fire_amount_total = 0;
    public int freeze_amount_total = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        prefabDamageText = GameObject.FindWithTag("DamageText");
        XPprefab = GameObject.FindWithTag("XP");
    }

    public void receiveFirePoint(int fire_amount){
        fire_amount_total += fire_amount;
    }

    public void receiveFreezePoint(int freeze_amount){
        freeze_amount_total += freeze_amount;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x,3*currentHealth/maxHealth);
        Quaternion direction = Quaternion.Euler(90, 0, 0);
        float x_Range = Random.Range(0, 2);
        Vector3 offset = new Vector3(x_Range,2f,0f);

        GameObject newDamage = Instantiate(prefabDamageText, transform.position+offset, direction);
        TMPro.TextMeshPro newDamage_text = newDamage.GetComponent<TMPro.TextMeshPro>();

        newDamage_text.text = damageAmount + "";
        Destroy(newDamage, 1);
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
