using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LootMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject LootMenuUI;

    public bool IsGamePaused(){
        return GameIsPaused;
    }

    public void Resume_Game(){
        LootMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause_Game(){
        LootMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q")){
            if (GameIsPaused){
                Resume_Game();
            }
            else{
                Pause_Game();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume_Game();
            }
        }
        
    }
}
