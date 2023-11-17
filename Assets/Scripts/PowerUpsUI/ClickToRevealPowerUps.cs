using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToRevealPowerUps : MonoBehaviour
{
    public GameObject PowerUpOption1;
    public GameObject PowerUpOption2;
    public GameObject PowerUpOption3;

    public GameObject PowerUpClick;


    public void SortPowerUps(){
        string[] Option1_str_options = {"Danos com Explosão por 50"};
        string[] Option2_str_options = {"Aumento do Raio de Visão por 25"};
        string[] Option3_str_options = {"Mais dano por 5", "Atirar mais rápido por 5", 
                                        "Aumentar velocidade por 5", "Balas maiores por 5"};

        int index_1 = Random.Range(0, Option1_str_options.Length);
        int index_2 = Random.Range(0, Option2_str_options.Length);
        int index_3 = Random.Range(0, Option3_str_options.Length);

        GameObject text_PowerUpOption1 = PowerUpOption1.transform.Find("Text").gameObject;
        TMPro.TextMeshProUGUI option_1_tex = text_PowerUpOption1.GetComponent<TMPro.TextMeshProUGUI>();
        option_1_tex.text = Option1_str_options[index_1];

        GameObject text_PowerUpOption2 = PowerUpOption2.transform.Find("Text").gameObject;
        TMPro.TextMeshProUGUI option_2_tex = text_PowerUpOption2.GetComponent<TMPro.TextMeshProUGUI>();
        option_2_tex.text = Option2_str_options[index_2];

        GameObject text_PowerUpOption3 = PowerUpOption3.transform.Find("Text").gameObject;
        TMPro.TextMeshProUGUI option_3_tex = text_PowerUpOption3.GetComponent<TMPro.TextMeshProUGUI>();
        option_3_tex.text = Option3_str_options[index_3];
    }


    public void ClickToOpenOptions(){
        PowerUpClick.SetActive(false);
        PowerUpOption1.SetActive(true);
        PowerUpOption2.SetActive(true);
        PowerUpOption3.SetActive(true);

        SortPowerUps();
    }
}
