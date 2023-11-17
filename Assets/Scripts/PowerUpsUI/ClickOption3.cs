using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOption3 : MonoBehaviour
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

    public void ClickOption3Button(){
        if (player_exp.player_exp >= 5){
            player_exp.player_exp -= 5;
            GameObject ButtonText = gameObject.transform.Find("Text").gameObject;
            TMPro.TextMeshProUGUI option_3_tex = ButtonText.GetComponent<TMPro.TextMeshProUGUI>();
            string choice = option_3_tex.text;

            if(choice == "Mais dano por 5"){
                player_exp.power_up_bullet_damage += 1;
            }
            if(choice == "Atirar mais rápido por 5"){
                player_exp.power_up_fire_rate_increase += 1;
            }
            if(choice == "Aumentar velocidade por 5"){
                player_exp.power_up_speed_increase += 1;
            }
            if(choice == "Balas maiores por 5"){
                player_exp.power_up_bullet_size += 1;
            }

            open_power_up.PowerUpActivated = false;
        }
    }
}
