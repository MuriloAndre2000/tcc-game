using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChoosing : MonoBehaviour
{
    private Camera camera_1;
    public static int Weapon_ID;
    private GameObject Canvas;
    private GameObject Select_Weapon;
    private RectTransform Select_Weapon_Transform;
    // Start is called before the first frame update
    void Start()
    {
        Weapon_ID = 1;
        camera_1 =  Camera.main;
        Change_Camera();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")){
             Weapon_ID = 1;
             Change_Camera();
        }
        if(Input.GetKeyDown("2")){
             Weapon_ID = 2;
             Change_Camera();
        }
        if(Input.GetKeyDown("3")){
             Weapon_ID = 3;
             Change_Camera();
        }
    }

    void Change_Camera(){
        //camera = gameObject.transform.Find("Main Camera").gameObject;
        Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
        Select_Weapon = Canvas.transform.Find("Select_Weapon").gameObject;
        Select_Weapon_Transform = Select_Weapon.GetComponent<RectTransform>();
        int posX = -140;
        if (Weapon_ID == 1){
            posX = -140;
        }
        if (Weapon_ID == 2){
            posX = 0;
        }
        if (Weapon_ID == 3){
            posX = 140;
        }
        Select_Weapon_Transform.anchoredPosition  = new Vector2(posX,-200);// trocar por Posição Relativa
    }
}
