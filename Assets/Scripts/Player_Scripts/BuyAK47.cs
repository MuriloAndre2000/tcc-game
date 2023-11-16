using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuyAK47 : MonoBehaviour
{
    public GameObject BuyText;
    private bool activate = false;
    private bool buy_weapon = true;
    private bool buy_weapon_munition = true;
    PlayerWeaponChoosing player_weapon_choosing;
    GameObject weapon;
    Transform object_transform;
    string str_machine_gun = "machine_gun";
    WeaponMunitionHandler weapon_munition_handler;

    void Start(){
        player_weapon_choosing = gameObject.GetComponent<PlayerWeaponChoosing>();
        weapon = GameObject.FindWithTag("AK47");
        object_transform = weapon.transform;
        weapon_munition_handler = gameObject.GetComponent<WeaponMunitionHandler>();
    }


    // Update is called once per frame
    void Update()
    {
        activate = false;
        int pos_machine_gun = Array.IndexOf(player_weapon_choosing.weapon_list, str_machine_gun);
        float distance = Vector3.Distance(object_transform.position, transform.position);
        if (distance < 2){
            activate = true;
            if (pos_machine_gun == -1){
                buy_weapon = true;
                }
            else{
                buy_weapon = false;
                if(weapon_munition_handler.machine_gun == weapon_munition_handler.max_machine_gun){
                    buy_weapon_munition = false;
                }
                else{        
                    buy_weapon_munition = true;
                }
            }
            if(Input.GetKeyDown("e")){
                PlayerEXP player_exp = gameObject.GetComponent<PlayerEXP>();
                if (pos_machine_gun == -1){
                    if (player_exp.player_exp >=20){
                        player_exp.player_exp -= 20;
                        player_weapon_choosing.BuyWeapon(str_machine_gun);
                        weapon_munition_handler.machine_gun = weapon_munition_handler.max_machine_gun;
                    }
                }
                else{
                    buy_weapon = false;
                    if(weapon_munition_handler.machine_gun != weapon_munition_handler.max_machine_gun){
                        if (player_exp.player_exp >=10){
                            player_exp.player_exp -= 10;
                            weapon_munition_handler.machine_gun = weapon_munition_handler.max_machine_gun;
                        }
                    }
                }
            }
        }
    
        if(activate){
            TMPro.TextMeshProUGUI buy_text = BuyText.GetComponent<TMPro.TextMeshProUGUI>();
            if(buy_weapon){
                buy_text.text = "Aperte E para\nComprar AK47 por 20";
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
