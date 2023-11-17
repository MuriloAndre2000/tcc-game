using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOption1 : MonoBehaviour
{
    public GameObject PowerUpsHandler;
    private OpenPowerUp open_power_up;

    private GameObject player;
    private PlayerEXP player_exp;

    void Start(){
        open_power_up = PowerUpsHandler.GetComponent<OpenPowerUp>();
        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();
    }

    public void ClickOption1Button(){
        if (player_exp.player_exp >= 50){
            player_exp.player_exp -= 50;
            GameObject ButtonText = gameObject.transform.Find("Text").gameObject;
            TMPro.TextMeshProUGUI option_1_tex = ButtonText.GetComponent<TMPro.TextMeshProUGUI>();
            string choice = option_1_tex.text;

            if(choice == "Danos com Explosão por 50"){
                player_exp.power_up_explosive_bullet += 1;
            }

            open_power_up.PowerUpActivated = false;
        }
    }
}
