using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerWeaponChoosing : MonoBehaviour
{
    private Camera camera_1;
    public static int Weapon_ID;
    public static int Weapon_Slot_ID;

    private GameObject Canvas;
    private GameObject Select_Weapon;
    private RectTransform Select_Weapon_Transform;

    public GameObject WeaponMunition;
    
    public string[] weapon_list = {"pistol"};

    public GameObject pistol;
    public GameObject machine_gun;
    public GameObject shotgun;
    public GameObject granade;
    public GameObject RPG;

    string str_pistol      = "pistol";
    string str_machine_gun = "machine_gun";
    string str_shotgun     = "shotgun";
    string str_granade     = "granade";
    string str_RPG         = "RPG";

    // Start is called before the first frame update
    // Weapon_ID
    // 1: Pistol
    // 2: Machine Gun
    // 3: Shotgun
    // 4: Granade
    // 5: RPG
    void Start()
    {
        Weapon_ID = 1;
        Weapon_Slot_ID = 1;
        camera_1 =  Camera.main;
        Change_Select_Weapon();
        Change_Weapons_In_UI();
        Change_Weapons();
    }

    // Update is called once per frame
    void Update()
    {
        Change_Weapons_In_UI();
        Change_Weapons();
        Change_Select_Weapon();
        Show_Munition_Weapons();
        if(Input.GetKeyDown("1")){
             Weapon_Slot_ID = 1;
             Change_Select_Weapon();
        }
        if(Input.GetKeyDown("2")){
             Weapon_Slot_ID = 2;
             Change_Select_Weapon();
        }
        if(Input.GetKeyDown("3")){
             Weapon_Slot_ID = 3;
             Change_Select_Weapon();
        }
    }

    void Show_Munition_Weapons(){
        int pos_pistol      = Array.IndexOf(weapon_list, str_pistol);
        int pos_machine_gun = Array.IndexOf(weapon_list, str_machine_gun);
        int pos_shotgun     = Array.IndexOf(weapon_list, str_shotgun);
        int pos_granade     = Array.IndexOf(weapon_list, str_granade);
        int pos_RPG         = Array.IndexOf(weapon_list, str_RPG);

        WeaponMunitionHandler weapon_munition_handler = gameObject.GetComponent<WeaponMunitionHandler>();

        if (Weapon_Slot_ID - 1 == pos_pistol){
            TMPro.TextMeshProUGUI weapon_munition_text = WeaponMunition.GetComponent<TMPro.TextMeshProUGUI>();
            weapon_munition_text.text = "∞/∞";
        }   
        if (Weapon_Slot_ID - 1 == pos_machine_gun){
            TMPro.TextMeshProUGUI weapon_munition_text = WeaponMunition.GetComponent<TMPro.TextMeshProUGUI>();
            weapon_munition_text.text = weapon_munition_handler.machine_gun + "/" + weapon_munition_handler.max_machine_gun; 
        }   
        if (Weapon_Slot_ID - 1 == pos_shotgun){
            TMPro.TextMeshProUGUI weapon_munition_text = WeaponMunition.GetComponent<TMPro.TextMeshProUGUI>();
            weapon_munition_text.text = weapon_munition_handler.shotgun + "/" + weapon_munition_handler.max_shotgun;
        }   
        if (Weapon_Slot_ID - 1 == pos_granade){
            TMPro.TextMeshProUGUI weapon_munition_text = WeaponMunition.GetComponent<TMPro.TextMeshProUGUI>();
            weapon_munition_text.text = weapon_munition_handler.granade + "/" + weapon_munition_handler.max_granade;
        }   
        if (Weapon_Slot_ID - 1 == pos_RPG){
            TMPro.TextMeshProUGUI weapon_munition_text = WeaponMunition.GetComponent<TMPro.TextMeshProUGUI>();
            weapon_munition_text.text = weapon_munition_handler.rpg + "/" + weapon_munition_handler.max_rpg;
        }        
    }

    void Change_Weapons(){

        int pos_pistol      = Array.IndexOf(weapon_list, str_pistol);
        int pos_machine_gun = Array.IndexOf(weapon_list, str_machine_gun);
        int pos_shotgun     = Array.IndexOf(weapon_list, str_shotgun);
        int pos_granade     = Array.IndexOf(weapon_list, str_granade);
        int pos_RPG         = Array.IndexOf(weapon_list, str_RPG);

        if (Weapon_Slot_ID - 1 == pos_pistol){
            Weapon_ID = 1;
        }   
        if (Weapon_Slot_ID - 1 == pos_machine_gun){
            Weapon_ID = 2;
        }   
        if (Weapon_Slot_ID - 1 == pos_shotgun){
            Weapon_ID = 3;
        }   
        if (Weapon_Slot_ID - 1 == pos_granade){
            Weapon_ID = 4;
        }   
        if (Weapon_Slot_ID - 1 == pos_RPG){
            Weapon_ID = 5;
        }        
    }

    void Change_Weapons_In_UI(){
        int pos_pistol      = Array.IndexOf(weapon_list, str_pistol);
        int pos_machine_gun = Array.IndexOf(weapon_list, str_machine_gun);
        int pos_shotgun     = Array.IndexOf(weapon_list, str_shotgun);
        int pos_granade     = Array.IndexOf(weapon_list, str_granade);
        int pos_RPG         = Array.IndexOf(weapon_list, str_RPG);

        RectTransform pistol_transform      = pistol.GetComponent<RectTransform>();
        RectTransform machine_gun_transform = machine_gun.GetComponent<RectTransform>();
        RectTransform shotgun_transform     = shotgun.GetComponent<RectTransform>();
        RectTransform granade_transform     = granade.GetComponent<RectTransform>();
        RectTransform RPG_transform         = RPG.GetComponent<RectTransform>();

        pistol_transform.anchoredPosition       = new Vector2(10000,-200);
        machine_gun_transform.anchoredPosition  = new Vector2(10000,-200);
        shotgun_transform.anchoredPosition      = new Vector2(10000,-200);
        granade_transform.anchoredPosition      = new Vector2(10000,-200);
        RPG_transform.anchoredPosition          = new Vector2(10000,-200);

        if(weapon_list.Length == 1){
            int posX = 0;
            if(pos_pistol == 0){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 0){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 0){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 0){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 0){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
        }
        if(weapon_list.Length == 2){
            int posX = -70;
            if(pos_pistol == 0){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 0){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 0){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 0){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 0){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            posX = 70;
            if(pos_pistol == 1){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 1){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 1){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 1){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 1){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
        }
        if(weapon_list.Length == 3){
            int posX = -140;
            if(pos_pistol == 0){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 0){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 0){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 0){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 0){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            posX = 0;
            if(pos_pistol == 1){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 1){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 1){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 1){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 1){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            posX = 140;
            if(pos_pistol == 2){
                pistol_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_machine_gun == 2){
                machine_gun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_shotgun == 2){
                shotgun_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_granade == 2){
                granade_transform.anchoredPosition  = new Vector2(posX,-200);
            }
            if(pos_RPG == 2){
                RPG_transform.anchoredPosition  = new Vector2(posX,-200);
            }
        }
    }

    void Change_Select_Weapon(){
        //camera = gameObject.transform.Find("Main Camera").gameObject;
        Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
        Select_Weapon = Canvas.transform.Find("Select_Weapon").gameObject;
        Select_Weapon_Transform = Select_Weapon.GetComponent<RectTransform>();
        int posX = -140;
        if(weapon_list.Length == 1){
            posX = 0;
        }
        else if(weapon_list.Length == 2){
            if (Weapon_Slot_ID == 1){
                posX = -70;
            }
            if (Weapon_Slot_ID == 2){
                posX = 70;
            }
        }
        else{
            if (Weapon_Slot_ID == 1){
                posX = -140;
            }
            if (Weapon_Slot_ID == 2){
                posX = 0;
            }
            if (Weapon_Slot_ID == 3){
                posX = 140;
            }
        }
        Select_Weapon_Transform.anchoredPosition  = new Vector2(posX,-200);// trocar por Posição Relativa
    }

    public void BuyWeapon(string weapon_str){
        if(weapon_list.Length == 1){
            weapon_list = new string[] {weapon_list[0], weapon_str};
        }
        else if(weapon_list.Length == 2){
            weapon_list = new string[] {weapon_list[0], weapon_list[1], weapon_str};
        }
        else{
            if (Weapon_Slot_ID == 1){
                weapon_list[0] = weapon_str;
            }
            if (Weapon_Slot_ID == 2){
                weapon_list[1] = weapon_str;
            }
            if (Weapon_Slot_ID == 3){
                weapon_list[2] = weapon_str;
            }
        }
    }
}
