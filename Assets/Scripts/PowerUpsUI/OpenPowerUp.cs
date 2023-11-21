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

    public GameObject XPprefab;

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
            foreach(GameObject object_XP in GameObject.FindGameObjectsWithTag("XP")){
                XPMovement xp_movement = object_XP.GetComponent<XPMovement>();
                xp_movement.distance_inital = 1000f;
                xp_movement.movementSpeed = 10;
            }
        }
        else{
            GameIsPaused = false;
            PowerUpMenuUI.SetActive(false);
            foreach(GameObject object_XP in GameObject.FindGameObjectsWithTag("XP")){
                XPMovement xp_movement = object_XP.GetComponent<XPMovement>();
                xp_movement.distance_inital = xp_movement.min_distance_inital;
                xp_movement.movementSpeed = xp_movement.inital_movementSpeed;
            }
        }
        
    }
}
