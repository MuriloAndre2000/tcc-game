using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOption2 : MonoBehaviour
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

    public void ClickOption2Button(){
        if (player_exp.player_exp >= 25){
            player_exp.player_exp -= 25;
            GameObject ButtonText = gameObject.transform.Find("Text").gameObject;
            TMPro.TextMeshProUGUI option_2_tex = ButtonText.GetComponent<TMPro.TextMeshProUGUI>();
            string choice = option_2_tex.text;

            if(choice == "Aumento do Raio de Visão por 25"){
                player_exp.power_up_field_radius += 1;
            }

            open_power_up.PowerUpActivated = false;
        }
    }
}
