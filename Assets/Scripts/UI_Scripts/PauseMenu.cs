using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool OptionIsOpen = false;

    public GameObject PauseMenuUI;
    public GameObject OptionMenuUI;

    public bool IsGamePaused(){
        return GameIsPaused;
    }

    public void Resume_Game(){
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause_Game(){
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void OpenOptionMenu(){
        PauseMenuUI.SetActive(false);
        OptionMenuUI.SetActive(true);
        OptionIsOpen = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackToPauseMenu(){
        PauseMenuUI.SetActive(true);
        OptionMenuUI.SetActive(false);
        OptionIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                if(OptionIsOpen){
                    BackToPauseMenu();
                }
                else{
                    Resume_Game();
                }
            }
            else{
                Pause_Game();
            }
        }
        
    }
}
