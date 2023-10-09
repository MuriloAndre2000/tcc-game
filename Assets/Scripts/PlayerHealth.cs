using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
        GameObject Canvas =  Camera.main.gameObject.transform.Find("Canvas").gameObject;
        GameObject You_Died = Canvas.transform.Find("You_Died").gameObject;
        RectTransform You_Died_transform = You_Died.GetComponent<RectTransform>();
        You_Died_transform.anchoredPosition  = new Vector2(0,200);// trocar por Posição Relativa

        string url = "https://southamerica-east1-disco-bedrock-364702.cloudfunctions.net/get_best_waves"; 

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.SendWebRequest();

        int i = 0;
        while(request.result == UnityWebRequest.Result.InProgress){
            i += 1; // Just to do Something
            if (i > 10000){
                i = 0;
            }
        }
        Debug.Log(i);
        Debug.Log("POST request successful!");
        Debug.Log(request.downloadHandler.text); // Response from server
        

        GameObject You_Died_Scores = Canvas.transform.Find("You_Died_Scores").gameObject;
        RectTransform You_Died_Scores_transform = You_Died_Scores.GetComponent<RectTransform>();
        TMPro.TextMeshProUGUI You_Died_Scores_text = You_Died_Scores.GetComponent<TMPro.TextMeshProUGUI>();
        You_Died_Scores_text.text = "Melhores Scores" + "\n" + request.downloadHandler.text;
        You_Died_Scores_transform.anchoredPosition  = new Vector2(0,85);// trocar por Posição Relativa

        GameObject You_Died_Back_Main_Menu = Canvas.transform.Find("You_Died_Back_Main_Menu").gameObject;
        RectTransform You_Died_Back_Main_Menu_transform = You_Died_Back_Main_Menu.GetComponent<RectTransform>();
        You_Died_Back_Main_Menu_transform.anchoredPosition  = new Vector2(0,-210);// trocar por Posição Relativa

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
