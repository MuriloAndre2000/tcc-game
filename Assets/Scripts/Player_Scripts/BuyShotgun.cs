using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuyShotgun : MonoBehaviour
{
    public GameObject BuyText;
    private bool activate = false;
    private bool buy_weapon = true;
    private bool buy_weapon_munition = true;
    PlayerWeaponChoosing player_weapon_choosing;
    GameObject weapon;
    Transform object_transform;
    string str_shotgun = "shotgun";
    WeaponMunitionHandler weapon_munition_handler;

    void Start(){
        player_weapon_choosing = gameObject.GetComponent<PlayerWeaponChoosing>();
        weapon = GameObject.FindWithTag("Shotgun");
        object_transform = weapon.transform;
        weapon_munition_handler = gameObject.GetComponent<WeaponMunitionHandler>();
    }


    // Update is called once per frame
    void Update()
    {
        activate = false;
        int pos_shotgun = Array.IndexOf(player_weapon_choosing.weapon_list, str_shotgun);
        float distance = Vector3.Distance(object_transform.position, transform.position);
        if (distance < 2){
            activate = true;
            if (pos_shotgun == -1){
                buy_weapon = true;
                }
            else{
                buy_weapon = false;
                if(weapon_munition_handler.shotgun == weapon_munition_handler.max_shotgun){
                    buy_weapon_munition = false;
                }
                else{        
                    buy_weapon_munition = true;
                }
            }
            if(Input.GetKeyDown("e")){
                PlayerEXP player_exp = gameObject.GetComponent<PlayerEXP>();
                if (pos_shotgun == -1){
                    if (player_exp.player_exp >=20){
                        player_exp.player_exp -= 20;
                        player_weapon_choosing.BuyWeapon(str_shotgun);
                        weapon_munition_handler.shotgun = weapon_munition_handler.max_shotgun;
                    }
                }
                else{
                    buy_weapon = false;
                    if(weapon_munition_handler.shotgun != weapon_munition_handler.max_shotgun){
                        if (player_exp.player_exp >=10){
                            player_exp.player_exp -= 10;
                            weapon_munition_handler.shotgun = weapon_munition_handler.max_shotgun;
                        }
                    }
                }
            }
        }
    
        if(activate){
            TMPro.TextMeshProUGUI buy_text = BuyText.GetComponent<TMPro.TextMeshProUGUI>();
            if(buy_weapon){
                buy_text.text = "Aperte E para\nComprar Shotgun por 20";
            }
            else{
                if(buy_weapon_munition){
                    buy_text.text = "Aperte E para\nComprar munição por 10";
                }
                else{
                    buy_text.text = "Munição cheia";
                }
            }
        }
        BuyText.SetActive(activate);

    }
}
