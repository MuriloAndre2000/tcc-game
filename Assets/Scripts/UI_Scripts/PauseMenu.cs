using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public bool IsGamePaused(){
        return GameIsPaused;
    }

    void Resume_Game(){
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    void Pause_Game(){
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume_Game();
            }
            else{
                Pause_Game();
            }
        }
        
    }
}
