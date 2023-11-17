using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPowerUp : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool PowerUpActivated = false;

    public GameObject PowerUpMenuUI;
    public GameObject PowerUpButtonClick;
    public GameObject PowerUpOption1;
    public GameObject PowerUpOption2;
    public GameObject PowerUpOption3;

    public bool IsGamePaused(){
        return GameIsPaused;
    }

    public void OpenPowerUpMenu(){
        PowerUpActivated = true;
        PowerUpMenuUI.SetActive(true);
        PowerUpButtonClick.SetActive(true);
        PowerUpOption1.SetActive(false);
        PowerUpOption2.SetActive(false);
        PowerUpOption3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PowerUpActivated){
            GameIsPaused = true;
            if(Input.GetKeyDown(KeyCode.Escape)){
                PowerUpActivated = false;
            }
        }
        else{
            GameIsPaused = false;
            PowerUpMenuUI.SetActive(false);
        }
        
    }
}
