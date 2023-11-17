using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private GameObject player;
    private PlayerEXP player_exp;

    private GameObject Canvas;
    private GameObject Pause_Menu;
    private PauseMenu PauseMenu_Object;

    private GameObject Loot_Menu;
    private LootMenu LootMenu_Object;

    public GameObject PowerUP_Menu;
    private OpenPowerUp PowerUP_Object;

    public bool GameIsPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();

        Canvas = Camera.main.gameObject.transform.Find("Canvas").gameObject;
        Pause_Menu = Camera.main.gameObject.transform.Find("Pause").gameObject;
        PauseMenu_Object = Pause_Menu.GetComponent<PauseMenu>();

        Loot_Menu = Camera.main.gameObject.transform.Find("Loot").gameObject;
        LootMenu_Object = Loot_Menu.GetComponent<LootMenu>();

        PowerUP_Object = PowerUP_Menu.GetComponent<OpenPowerUp>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player_exp.return_pause_all() == true){
            GameIsPaused = true;
        }
        else if(PauseMenu_Object.IsGamePaused() == true){
            GameIsPaused = true;
        }
        else if(LootMenu_Object.IsGamePaused() == true){
            GameIsPaused = true;
        }
        else if(PowerUP_Object.IsGamePaused() == true){
            GameIsPaused = true;
        }
        else{
            GameIsPaused = false;
        }
        
    }
}
